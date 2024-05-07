// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.UserHandler.UserLoader
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.Base;
using QuizTop.Data.DataStruct.UserStruct;
using System.Text.Json;

#nullable enable
namespace QuizTop.Data.DataHandlers.UserHandler
{
    public static class UserLoader
    {
        private static bool DataHandled;

        public static void LoadUserDataBase()
        {
            LoadUserFromTitles(Directory.GetFiles(Application.DataBasePaths[typeof(UserDataBase)], UserSaver.GetSearchMaskUser()));
            DataHandled = true;
            EventBus.Publish("LoadUsersCompleted");
        }

        public static User? TryGetOrLoadUser(string userName)
        {
            string[] files = Directory.GetFiles(Application.DataBasePaths[typeof(UserDataBase)], UserSaver.GetSearchMaskUser(userName));
            User? user = null;
            if (UserDataBase.CheckUserByLogin(userName) || UserLoader.LoadUserFromTitles(files))
                user = UserDataBase.UsersByLogin[userName];
            return user;
        }

        public static bool LoadUserFromTitles(string[] titles)
        {
            bool flag = false;
            foreach (string title in titles)
            {
                try
                {
                    User? user = JsonSerializer.Deserialize<User>(File.ReadAllText(title));
                    if (user != null)
                    {
                        UserDataBase.AddUser(user);
                        flag = true;
                    }
                }
                catch { }
            }
            return flag;
        }

        public static bool CheckUserByLogin(string login) => Directory.GetFiles(Application.DataBasePaths[typeof(UserDataBase)], UserSaver.GetSearchMaskUser(login)).Length != 0;
    }
}
