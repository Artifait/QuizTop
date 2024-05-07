// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.UserHandler.UserSaver
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.Base;
using QuizTop.Data.DataStruct.UserStruct;
using System.Text.Json;

#nullable enable
namespace QuizTop.Data.DataHandlers.UserHandler
{
    public class UserSaver
    {
        public static void SaveUserDataBase()
        {
            foreach (User user in UserDataBase.UsersByLogin.Values)
                SaveUser(user);
            EventBus.Publish("SaveUsersCompleted");
        }

        public static void SaveUser(User user)
        {
            string path = Application.DataBasePaths[typeof(UserDataBase)] + GetUserFileName(user.UserName);
            try
            {
                string contents = JsonSerializer.Serialize<User>(user);
                File.WriteAllText(path, contents);
            }
            catch (Exception ex) { EventBus.Publish("Error", ex); }
        }

        public static string GetUserFileName(string userName) => GetSearchMaskUser(userName);
        public static string GetSearchMaskUser(string login = "*") => $"User_{login}_Quiz.json";
    }
}
