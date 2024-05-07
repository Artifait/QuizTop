// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.UserRecordHandler.UserRecordSaver
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.Base;
using QuizTop.Data.DataStruct.QuizStruct;
using QuizTop.Data.DataStruct.UserRecordStruct;
using System.Text.Json;

#nullable enable
namespace QuizTop.Data.DataHandlers.UserRecordHandler
{
    public class UserRecordSaver
    {
        public static JsonSerializerOptions optionsSaver = new() { WriteIndented = true };

        public static void SaveUserRecordDataBase()
        {
            foreach (var DictLogin in UserRecordDataBase.Records.Values)
                SaveUserRecord(DictLogin);
        }
        //сохраняет рекорды user по всем предметам
        public static void SaveUserRecord( Dictionary<Subject, Dictionary<int, UserRecord>> DictLogin)
        {
            foreach (var DictSubject in DictLogin.Values)
                SaveUserRecord(DictSubject);
        }
        //сохраняет рекорды user по предмету
        public static void SaveUserRecord(Dictionary<int, UserRecord> DictSubject)
        {
            foreach (var record in DictSubject.Values)
                SaveUserRecord(record);
        }

        public static void SaveUserRecord(UserRecord record)
        {
            string path = Application.DataBasePaths[typeof(UserRecordDataBase)] + GetUserRecordFileName(record);
            try
            {
                string contents = JsonSerializer.Serialize(record, optionsSaver);
                File.WriteAllText(path, contents);
            }
            catch (Exception ex) { EventBus.Publish("Error", ex); }
        }

        public static string GetUserRecordFileName(UserRecord record) => GetUserRecordFileName(record.UserName, record.QuizTitle);
        public static string GetUserRecordFileName(string userName, string quizTitle) => GetSearchMaskUserRecord(userName, quizTitle);

        public static string GetSearchMaskUserRecord(string login = "*", string quizTitle = "*") => $"UserRecord_{login}_{quizTitle}.json";

    }
}
