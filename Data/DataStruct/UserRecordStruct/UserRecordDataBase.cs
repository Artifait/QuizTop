// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataStruct.UserRecordStruct.UserRecordDataBase
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.QuizStruct;

#nullable enable
namespace QuizTop.Data.DataStruct.UserRecordStruct
{
    public class UserRecordDataBase
    {
        public static Dictionary<string, Dictionary<Subject, Dictionary<int, UserRecord>>> Records = [];
        public static Dictionary<string, List<UserRecord>> RecordsByLogin = [];
        public static Dictionary<string, List<UserRecord>> RecordsByTitle = [];

        public static void CreateOrUpdateRecord(UserRecord record)
        {
            if (record == null)
                return;
            string userName = record.UserName;
            Subject quizSubject = record.quizSubject;
            string quizTitle = record.QuizTitle;
            int idQuiz = record.IdQuiz;


            if (!Records.ContainsKey(userName))
                Records[userName] = [];
            if (!Records[userName].ContainsKey(quizSubject))
                Records[userName][quizSubject] = [];
            Records[userName][quizSubject][idQuiz] = record;


            if (!RecordsByLogin.ContainsKey(userName)) 
                RecordsByLogin[userName] = [];
            RecordsByLogin[userName].Add(record);


            if (!RecordsByTitle.ContainsKey(userName))
                RecordsByTitle[quizTitle] = [];
            RecordsByTitle[quizTitle].Add(record);
        }

        public static List<UserRecord> GetAllRecordsByLogin(string Login)
        {
            if (!RecordsByLogin.ContainsKey(Login))
                RecordsByLogin[Login] = [];
            return RecordsByLogin[Login];
        }

        public static List<UserRecord> GetAllRecordsByQuizTitle(string quizTitle)
        {
            if (!RecordsByTitle.ContainsKey(quizTitle))
                RecordsByLogin[quizTitle] = [];
            return RecordsByTitle[quizTitle];
        }
    }
}
