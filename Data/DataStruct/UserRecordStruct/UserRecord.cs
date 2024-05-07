// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataStruct.UserRecordStruct.UserRecord
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.QuizStruct;

#nullable enable
namespace QuizTop.Data.DataStruct.UserRecordStruct
{
    [Serializable]
    public class UserRecord
    {
        public string UserName { get; set; } = string.Empty;
        public string QuizTitle { get; set; } = string.Empty;

        public TimeSpan RecordTime { get; set; }
        public DateTime RecordDate { get; set; }
        public Subject quizSubject { get; set; }

        public int Grade { get; set; }
        public int IdQuiz { get; set; }
        public int IdQuizSubject { get; set; }

        public override string ToString() => $"User: {UserName}.\nQuiz: {QuizTitle}.\nGrade: {Grade}.";
    }
}
