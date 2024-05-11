// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.QuestionHandler.QuestionDataBaseSaver
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.Base;
using QuizTop.Data.DataStruct.QuestionStruct;
using QuizTop.Data.DataStruct.QuizStruct;
using System.Runtime.CompilerServices;
using System.Text.Json;

#nullable enable
namespace QuizTop.Data.DataHandlers.QuestionHandler
{
    public static class QuestionDataBaseSaver
    {
        public static void SaveQuestionDataBase()
        {
            foreach (var question1 in QuestionDataBase.Questions)
            {
                foreach (Question question2 in question1.Value)
                    SaveQuestion(question2);
            }
            EventBus.Publish("SaveQuestionsCompleted");
        }

        public static void SaveQuestion(Question question)
        {
            string path = Application.DataBasePaths[typeof(QuestionDataBase)] + QuestionDataBaseSaver.GetQuestionFileName(question);
            try
            {
                string contents = JsonSerializer.Serialize(question);
                File.WriteAllText(path, contents);
            }
            catch (Exception ex) { EventBus.Publish("Error", (object)ex); }
        }

        private static string GetQuestionFileName(Question question) 
            => GetSearchMaskQuestion(question.IdQuestion.ToString(), Enum.GetName(question.questionTypes), question.IdQuestionOfSubject.ToString());


        public static string GetSearchMaskQuestion(string strId = "*", string strSubject = "*", string strIdOfSubject = "*") 
            => $"Question_{strId}_{strSubject}_{strIdOfSubject}.json";

        public static void SaveQuestionDateBaseInfo()
        {
            try { File.WriteAllText(QuestionDataBase.GetFileNameInfoDateBase(), JsonSerializer.Serialize(QuestionDataBase.InfoQuestionDataBase)); }
            catch{ }
        }
    }
}
