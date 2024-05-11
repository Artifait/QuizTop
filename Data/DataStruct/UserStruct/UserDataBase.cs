// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataStruct.UserStruct.UserDataBase
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.UserHandler;
using QuizTop.Data.DataHandlers.UserRecordHandler;
using QuizTop.Data.DataStruct.UserRecordStruct;
using QuizTop.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

#nullable enable
namespace QuizTop.Data.DataStruct.UserStruct
{
    public static class UserDataBase
    {
        public static Dictionary<string, User> UsersByLogin = [];

        public static string UserNowPath
        {
            get => Application.DataBasePaths[typeof(UserDataBase)] + UserSaver.GetUserFileName(Application.UserNow.UserName);
        }

        public static bool CheckUserByLogin(string login) => UsersByLogin.ContainsKey(login);
        public static bool CheckUserByLoginAndPath(string login) => UsersByLogin.ContainsKey(login) || UserLoader.CheckUserByLogin(login);

        public static void AddUser(User user) => UsersByLogin[user.UserName] = user;
        public static void AddNewUser(User user)
        {
            UsersByLogin[user.UserName] = user;
            UserSaver.SaveUser(user);
        }

        private static void UserPropertyValidater<T>(T nowValue, T newValue, string nameProperty) where T : IComparable<T>
        {
            if (newValue.CompareTo(nowValue) != 0) return;
            InputterData.propertyWarnings.TryGetValue(nameProperty, out int num);

            if (num != -1) throw new ArgumentException("ЗАЧЕМ МЕНЯТЬ НА ТАКОЙЖЕ\nВ ЧЁЁЁМ СМЫСЛ...\nИДИИ ПОДУМАЙ И ПРИХОДИ =D");
            throw new ArgumentException("ЗАЧЕМ МЕНЯТЬ НА ТАКОЙЖЕ\nВ ЧЁЁЁМ СМЫСЛ...\nИДИИ ПОДУМАЙ И ПРИХОДИ =D\nНОООО СПАСИБО ЧТО ВВЁЛ ХОТЬ ЧТОТО :->");
        }

        public static void ChangeLogin()
        {
            string newUserName = InputterData.InputPropertyWithWarningWin(nameof(User.UserName), out bool Warning);
            if (Warning || !User.ValidationUserName(newUserName))
                throw new ArgumentException("ИНVALIDНЫЕ ДАННЫЕ!");
            UserPropertyValidater(Application.UserNow.UserName, newUserName, nameof(User.UserName));
            UsersByLogin.Remove(Application.UserNow.UserName);

            try
            {
                if(UserRecordDataBase.RecordsByLogin.TryGetValue(Application.UserNow.UserName, out var records))
                {
                    for (int i = 0, cnt = records.Count; i < cnt; i++)
                    {
                        string fileName = Application.DataBasePaths[typeof(UserRecordDataBase)] + UserRecordSaver.GetUserRecordFileName(records[0]);
                        File.Delete(fileName);
                        UserRecord newUserRecord = records[0];
                        UserRecordAppender.DeleteRecord(records[0]);
                        newUserRecord.UserName = newUserName;
                        UserRecordAppender.AddNewRecord(newUserRecord);
                    }
                }
            }
            catch { }
            
            
            File.WriteAllText(Application.DataBasePaths[typeof(UserDataBase)] + UserSaver.GetUserFileName(newUserName), ChangePropertyUser(File.ReadAllText(UserNowPath), nameof(User.UserName), newUserName));
            File.Delete(UserNowPath);
            Application.UserNow.UserName = newUserName;
        }

        public static void ChangePassword()
        {
            string str = InputterData.InputPropertyWithWarningWin(nameof(User.Password), out bool Warning);
            if (Warning || !User.ValidationPassword(str))
                throw new ArgumentException("ИНVALIDНЫЕ ДАННЫЕ!");
            UserPropertyValidater(Application.UserNow.Password, str, nameof(User.Password));
            Application.UserNow.Password = str;
            File.WriteAllText(UserDataBase.UserNowPath, UserDataBase.ChangePropertyUser(File.ReadAllText(UserDataBase.UserNowPath), nameof(User.Password), str));
        }

        public static void ChangeDate()
        {
            DateTime newValue = InputterData.InputDateTimeHandlerErroreWin(nameof(User.DataBirth), out bool Warning);

            if (Warning || !User.ValidationDateBirth(newValue))
                throw new ArgumentException("ИНVALIDНЫЕ ДАННЫЕ!");

            UserPropertyValidater(Application.UserNow.DataBirth, newValue, nameof(User.DataBirth));
            string NewValue = JsonSerializer.Serialize(newValue);
            Application.UserNow.DataBirth = newValue;
            File.WriteAllText(UserNowPath, ChangePropertyUser(File.ReadAllText(UserDataBase.UserNowPath), nameof(User.DataBirth), NewValue));
        }

        private static string ChangePropertyUser(string jsonUser, string PropertyName, string NewValue)
        {
            JsonNode jsonNode = JsonSerializer.Deserialize<JsonNode>(jsonUser);
            jsonNode[PropertyName] = (JsonNode)NewValue;
            return jsonNode.ToJsonString();
        }
    }
}
