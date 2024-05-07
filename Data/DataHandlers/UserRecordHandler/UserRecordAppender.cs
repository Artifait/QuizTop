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
            UserRecordDataBase.CreateOrUpdateRecord(record);
            UserRecordSaver.SaveUserRecord(record);
        }
    }
}
