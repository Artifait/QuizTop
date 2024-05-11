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
        public static Dictionary<int, List<UserRecord>> RecordsByIdQuiz = [];

        public static void CreateOrUpdateRecord(UserRecord record)
        {
            if (record == null)
                return;

            string userName = record.UserName;
            Subject quizSubject = record.quizSubject;
            string quizTitle = record.QuizTitle;
            int idQuiz = record.IdQuiz;

            // Удаление дубликатов и оставление только записи с наивысшим Grade
            if (Records.ContainsKey(userName) && Records[userName].ContainsKey(quizSubject) && Records[userName][quizSubject].ContainsKey(idQuiz))
            {
                UserRecord existingRecord = Records[userName][quizSubject][idQuiz];
                if (record.Grade > existingRecord.Grade)
                {
                    Records[userName][quizSubject][idQuiz] = record;
                    RecordsByLogin[userName].Remove(existingRecord);
                    RecordsByIdQuiz[idQuiz].Remove(existingRecord);
                }
            }
            else
            {
                if (!Records.ContainsKey(userName))
                    Records[userName] = new Dictionary<Subject, Dictionary<int, UserRecord>>();
                if (!Records[userName].ContainsKey(quizSubject))
                    Records[userName][quizSubject] = new Dictionary<int, UserRecord>();

                Records[userName][quizSubject][idQuiz] = record;

                if (!RecordsByLogin.ContainsKey(userName))
                    RecordsByLogin[userName] = new List<UserRecord>();
                RecordsByLogin[userName].Add(record);

                if (!RecordsByIdQuiz.ContainsKey(idQuiz))
                    RecordsByIdQuiz[idQuiz] = new List<UserRecord>();
                RecordsByIdQuiz[idQuiz].Add(record);
            }
        }

        public static List<UserRecord> GetAllRecordsByLogin(string Login)
        {
            if (!RecordsByLogin.ContainsKey(Login))
                RecordsByLogin[Login] = [];
            return RecordsByLogin[Login];
        }

        public static List<UserRecord> GetAllRecordsByIdQuiz(int idQuiz)
        {
            if (!RecordsByIdQuiz.ContainsKey(idQuiz))
                RecordsByIdQuiz[idQuiz] = [];

            return RecordsByIdQuiz[idQuiz];
        }

        public static string GetFileNameInfoDateBase() => Application.DataBasePaths[typeof(UserRecordDataBase)] + "RecordDataBaseInfo.json";
    }
}
