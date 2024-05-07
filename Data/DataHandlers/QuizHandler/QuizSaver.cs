// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.QuizHandler.QuizDataBaseSaver
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.Base;
using QuizTop.Data.DataStruct.QuizStruct;
using System.Text.Json;

#nullable enable
namespace QuizTop.Data.DataHandlers.QuizHandler
{
    public class QuizDataBaseSaver
    {
        public static void SaveQuiz(Quiz quiz)
        {
            string path = Application.DataBasePaths[typeof(QuizDataBase)] + GetQuizFileName(quiz);
            try
            {
                string contents = JsonSerializer.Serialize(quiz);
                File.WriteAllText(path, contents);
            }
            catch (Exception ex) { EventBus.Publish("Error", ex); }
        }

        public static string GetQuizFileName(Quiz quiz) => GetSearchMaskQuiz(quiz.IdQuiz.ToString(), quiz.quizSubject.ToString(), quiz.IdQuiz.ToString());
        public static string GetSearchMaskQuiz(string strId = "*", string strSubject = "*", string strIdOfSubject = "*") => $"Quiz_{strId}_{strSubject}_{strIdOfSubject}.json";

        public static void SaveQuizDateBaseInfo()
        {
            try { File.WriteAllText(QuizDataBase.GetFileNameInfoDateBase(), JsonSerializer.Serialize<InfoQuizDataBase>(QuizDataBase.InfoQuizDataBase)); }
            catch { }
        }
    }
}
