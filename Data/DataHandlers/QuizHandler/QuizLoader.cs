// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.QuizHandler.QuizLoader
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.Base;
using QuizTop.Data.DataStruct.QuestionStruct;
using QuizTop.Data.DataStruct.QuizStruct;
using System.Text.Json;

#nullable disable
namespace QuizTop.Data.DataHandlers.QuizHandler
{
    public class QuizLoader
    {
        private static bool DataHandled;

        public static void LoadQuizDataBase()
        {
            LoadQuizDateBaseInfo();
            foreach (string file in Directory.GetFiles(Application.DataBasePaths[typeof(QuizDataBase)], QuizDataBaseSaver.GetSearchMaskQuiz()))
            {
                try
                {
                    Quiz quiz = JsonSerializer.Deserialize<Quiz>(File.ReadAllText(file));
                    if (quiz != null) QuizAppender.AddQuiz(quiz);
                }
                catch { }
            }
            DataHandled = true;
        }
        public static void LoadQuizDateBaseInfo()
        {
            InfoQuizDataBase quizDataBase;

            try { quizDataBase = JsonSerializer.Deserialize<InfoQuizDataBase>(File.ReadAllText(QuizDataBase.GetFileNameInfoDateBase())) ?? new InfoQuizDataBase(); }
            catch { quizDataBase = new InfoQuizDataBase(); }

            QuizDataBase.InfoQuizDataBase = quizDataBase;
        }
    }
}
