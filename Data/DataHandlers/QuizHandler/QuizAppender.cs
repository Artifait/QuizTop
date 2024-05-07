// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.QuizHandler.QuizAppender
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.QuizStruct;

#nullable enable
namespace QuizTop.Data.DataHandlers.QuizHandler
{
    public class QuizAppender
    {
        public static int AddNewQuiz(Quiz quiz)
        {
            quiz.IdQuiz = QuizDataBase.InfoQuizDataBase.CountQuiz++;
            quiz.IdQuizOfSubject = QuizDataBase.InfoQuizDataBase.CountQuizOfSubject[quiz.quizSubject]++;
            QuizDataBaseSaver.SaveQuizDateBaseInfo();
            QuizDataBaseSaver.SaveQuiz(quiz);
            AddQuiz(quiz);
            return quiz.IdQuiz;
        }

        public static void AddQuiz(Quiz quiz) => QuizDataBase.Quizs[quiz.quizSubject].Add(quiz);
    }
}
