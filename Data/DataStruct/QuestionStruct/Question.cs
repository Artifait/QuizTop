// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataStruct.QuestionStruct.Question
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.QuizStruct;


#nullable enable
namespace QuizTop.Data.DataStruct.QuestionStruct
{
    public enum TypeAnswer
    {
        RadioAnswer,
        MultiRadioAnswer,
        InputAnswer,
    }

    [Serializable]
    public class Question
    {
        public Subject questionTypes { get; set; }
        public TypeAnswer typeAnswer { get; set; }

        public string QuestionText { get; set; } = string.Empty;
        public Dictionary<int, string> AnswerVariants { get; set; } = [];
        public string AnswerOfQuestion { get; set; } = string.Empty;

        public int CountPoints { get; set; } = 0;
        public int IdQuestionOfSubject { get; set; }
        public int IdQuestion { get; set; }
    }
}
