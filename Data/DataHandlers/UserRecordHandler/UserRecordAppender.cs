// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.UserRecordHandler.UserRecordAppender
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.UserRecordStruct;

#nullable enable
namespace QuizTop.Data.DataHandlers.UserRecordHandler
{
    public static class UserRecordAppender
    {
        public static void AddNewRecord(UserRecord record)
        {
            UserRecordLoader.LoadUserRecord(UserRecordSaver.GetUserRecordFileName(record));
            CheckPath(record);
            
            if (UserRecordDataBase.Records[record.UserName][record.quizSubject].TryGetValue(record.IdQuiz, out var oldRecord))
            {
                if(oldRecord.Grade >= record.Grade)
                {
                    return;
                }
                DeleteRecord(record);
            }
            UserRecordDataBase.CreateOrUpdateRecord(record);
            UserRecordSaver.SaveUserRecord(record);
        }
        public static void DeleteRecord(UserRecord record)
        {
            CheckPath(record);
            UserRecordDataBase.Records[record.UserName][record.quizSubject].Remove(record.IdQuiz);
            UserRecordDataBase.RecordsByLogin[record.UserName].Remove(record);
            UserRecordDataBase.RecordsByIdQuiz[record.IdQuiz].Remove(record);
        }
        public static void CheckPath(UserRecord record)
        {
            if (!UserRecordDataBase.Records.ContainsKey(record.UserName))
                UserRecordDataBase.Records[record.UserName] = [];
            if (!UserRecordDataBase.Records[record.UserName].ContainsKey(record.quizSubject))
                UserRecordDataBase.Records[record.UserName][record.quizSubject] = [];

        }
    }
}
