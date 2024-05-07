// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.QuestionHandler.QuestionsAppender
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.QuestionStruct;

#nullable enable
namespace QuizTop.Data.DataHandlers.QuestionHandler
{
    public class QuestionsAppender
    {
        public static int AddNewQuestion(Question question)
        {
            question.IdQuestion = QuestionDataBase.InfoQuestionDataBase.CountQuestions++;
            question.IdQuestionOfSubject = QuestionDataBase.InfoQuestionDataBase.CountQuestionsOfSubject[question.questionTypes]++;

            QuestionDataBaseSaver.SaveQuestionDateBaseInfo();
            QuestionDataBaseSaver.SaveQuestion(question);

            AddQuestion(question);
            return question.IdQuestion;
        }

        public static void AddQuestion(Question question)
        {
            QuestionDataBase.Questions[question.questionTypes].Add(question);
            QuestionDataBase.QuestionsById[question.IdQuestion] = question;

        }
    }
}
