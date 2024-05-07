// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataStruct.QuizStruct.QuizDataBase
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.QuestionStruct;
using System;
using System.Collections.Generic;

#nullable enable
namespace QuizTop.Data.DataStruct.QuizStruct
{
    public enum Subject
    {
        Biology,
        Geography,
        History,
        Generator,
        AllEverything,
        CountSubject,
    }

    public class InfoQuizDataBase
    {
        public Dictionary<Subject, int> CountQuizOfSubject { get; set; } = new Dictionary<Subject, int>();
        public int CountQuiz { get; set; } = 0;


        public InfoQuizDataBase()
        {
            foreach (Subject key in Enum.GetValues(typeof(Subject))) CountQuizOfSubject.Add(key, 0);
        }
    }

    public static class QuizDataBase
    {
        public static Dictionary<Subject, List<Quiz>> Quizs = [];
        public static InfoQuizDataBase InfoQuizDataBase { get; set; } = new InfoQuizDataBase();

        static QuizDataBase()
        {
            foreach (Subject key in Enum.GetValues(typeof(Subject)))
                Quizs.Add(key, []);
        }

        public static List<Quiz> GetQuizFromPage(int page, int countQuiz, Subject quizSubject)
        {
            List<Quiz> quizFromPage = [];
            List<Quiz> quiz = Quizs[quizSubject];
            int index1 = page * countQuiz;
            for (int index2 = index1 + countQuiz; index1 < index2; ++index1)
            {
                try { quizFromPage.Add(quiz[index1]); }
                catch { break; }
            }
            return quizFromPage;
        }

        public static string GetFileNameInfoDateBase() => Application.DataBasePaths[typeof(QuestionDataBase)] + "QuizDataBaseInfo.json";
    }
}
