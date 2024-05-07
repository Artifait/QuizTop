// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.ApplicationWin.WinProfile
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.UserStruct;

#nullable enable
namespace QuizTop.UI.Win.ApplicationWin
{
    public class WinProfile : IWin
    {
        public WindowDisplay windowDisplay = new("Профиль", typeof(ProgramOptions), typeof(ProgramFields));
        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public Type? ProgramOptionsType => typeof(ProgramOptions);
        public Type? ProgramFieldsType => typeof(ProgramFields);

        public WinProfile()
        {
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Login), UserName);
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Password), Password);
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Data), DateBirth.ToShortDateString());
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Permission), Enum.GetName(Application.UserNow.Permission));
        }

        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

        public static string Password => Application.UserNow.Password;
        public static string UserName => Application.UserNow.UserName;
        public static DateTime DateBirth => Application.UserNow.DataBirth;


        public void Show() => windowDisplay.Show();
        public void InputHandler()
        {
            char lower = char.ToLower(Console.ReadKey().KeyChar);

            WindowTools.UpdateCursorPos(lower, ref windowDisplay, (int)ProgramOptions.CountOptions);

            if (WindowTools.IsKeySelect(lower)) HandlerMetodMenu();
        }

        private void HandlerMetodMenu()
        {
            Console.Clear();
            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.ChangeLogin:
                    UserDataBase.ChangeLogin();
                    windowDisplay.AddOrUpdateField("Login", UserName);
                    break;

                case ProgramOptions.ChangePasword:
                    UserDataBase.ChangePassword();
                    windowDisplay.AddOrUpdateField("Password", Password);
                    break;

                case ProgramOptions.ChangeData:
                    UserDataBase.ChangeDate();
                    windowDisplay.AddOrUpdateField("Data", DateBirth.ToShortDateString());
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        public enum ProgramOptions
        {
            ChangeLogin,
            ChangePasword,
            ChangeData,
            Back,
            CountOptions,
        }

        public enum ProgramFields
        {
            Login,
            Password,
            Data,
            Permission
        }
    }
}
