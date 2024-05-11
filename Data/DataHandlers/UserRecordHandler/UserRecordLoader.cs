// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.UserRecordHandler.UserRecordLoader
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.Base;
using QuizTop.Data.DataStruct.QuizStruct;
using QuizTop.Data.DataStruct.UserRecordStruct;
using System.IO;
using System.Text.Json;

#nullable enable
namespace QuizTop.Data.DataHandlers.UserRecordHandler
{
    public class UserRecordLoader
    {
        private static bool DataHandled;

        public static void LoadUserRecordDataBase()
        {
            string[] files = Directory.GetFiles(Application.DataBasePaths[typeof(UserRecordDataBase)], UserRecordSaver.GetSearchMaskUserRecord());
            if (files != null)
            {
                foreach (string fileName in files)
                    LoadUserRecord(fileName);
            }
            DataHandled = true;
            EventBus.Publish("LoadUsersCompleted");
        }

        public static void LoadUserRecords(string Login)
        {
            string[] files = Directory.GetFiles(Application.DataBasePaths[typeof(UserRecordDataBase)], UserRecordSaver.GetSearchMaskUserRecord(Login));

            if (files == null)
                return;
            foreach (string fileName in files)
                LoadUserRecord(fileName);

            if (!UserRecordDataBase.Records.ContainsKey(Login))
                UserRecordDataBase.Records[Login] = [];
            if (!UserRecordDataBase.RecordsByLogin.ContainsKey(Login))
                UserRecordDataBase.RecordsByLogin[Login] = [];
        }

        public static void LoadQuizUserRecords(int IdQuiz)
        {
            string[] files = Directory.GetFiles(Application.DataBasePaths[typeof(UserRecordDataBase)], UserRecordSaver.GetSearchMaskUserRecord(strQuizId: IdQuiz.ToString()));

            if (files == null)
                return;
            foreach (string fileName in files)
                LoadUserRecord(fileName);

            if (!UserRecordDataBase.RecordsByIdQuiz.ContainsKey(IdQuiz))
                UserRecordDataBase.RecordsByIdQuiz[IdQuiz] = [];
        }
        public static void LoadUserRecord(string fileName)
        {
            try
            {
                UserRecord? record = JsonSerializer.Deserialize<UserRecord>(File.ReadAllText(fileName), UserRecordSaver.optionsSaver);
                if (record == null)
                    return;
                UserRecordDataBase.CreateOrUpdateRecord(record);
            }
            catch{ }
        }
    }
}
