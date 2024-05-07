// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.QuestionHandler.QuestionLoader
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.Base;
using QuizTop.Data.DataStruct.QuestionStruct;
using System.IO;
using System.Text.Json;

#nullable disable
namespace QuizTop.Data.DataHandlers.QuestionHandler
{
    public class QuestionLoader
    {
        private static bool DataHandled;

        public static void LoadQuestionDataBase()
        {
            LoadQuestionDateBaseInfo();
            foreach (string file in Directory.GetFiles(Application.DataBasePaths[typeof(QuestionDataBase)], QuestionDataBaseSaver.GetSearchMaskQuestion()))
            {
                try
                {
                    Question question = JsonSerializer.Deserialize<Question>(File.ReadAllText(file));
                    if (question != null) QuestionsAppender.AddQuestion(question);
                }
                catch{ }
            }
            DataHandled = true;
            EventBus.Publish("LoadQuestionsCompleted");
        }

        public static void LoadQuestionDateBaseInfo()
        {
            InfoQuestionDataBase questionDataBase;

            try { questionDataBase = JsonSerializer.Deserialize<InfoQuestionDataBase>(File.ReadAllText(QuestionDataBase.GetFileNameInfoDateBase())) ?? new InfoQuestionDataBase(); }
            catch { questionDataBase = new InfoQuestionDataBase(); }

            QuestionDataBase.InfoQuestionDataBase = questionDataBase;
        }
    }
}
