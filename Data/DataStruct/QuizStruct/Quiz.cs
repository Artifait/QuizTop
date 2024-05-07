// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataStruct.QuizStruct.Quiz
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll


#nullable enable
namespace QuizTop.Data.DataStruct.QuizStruct
{
    [Serializable]
    public class Quiz
    {
        public Subject quizSubject { get; set; }
        public List<int> questionIdList { get; set; } = [];

        public string Title { get; set; } = string.Empty;
        public List<string> UserNamesTested { get; set; } = [];

        public int IdQuizOfSubject { get; set; }
        public int IdQuiz { get; set; }
    }
}
