// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataStruct.UserStruct.User
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll


#nullable enable
namespace QuizTop.Data.DataStruct.UserStruct
{
    public enum UserPermission
    {
        Admin,
        Moderator,
        User,
    }

    [Serializable]
    public class User
    {
        public UserPermission Permission { get; set; }
        public DateTime DataBirth { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public static bool ValidationPassword(string value) => !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
        public static bool ValidationUserName(string value) => !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value) && !UserDataBase.CheckUserByLoginAndPath(value);
        public static bool ValidationDateBirth(DateTime value)
        {
            TimeSpan timeSpan = DateTime.Now - value;
            return timeSpan.TotalDays <= 365 * 150 && timeSpan.TotalDays >= 0.0;
        }
    }
}
