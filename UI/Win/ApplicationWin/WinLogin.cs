// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.ApplicationWin.WinLogin
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.UserHandler;
using QuizTop.Data.DataStruct.UserStruct;
using QuizTop.UI.Win.QuizWin;

#nullable enable
namespace QuizTop.UI.Win.ApplicationWin
{
    public class WinLogin : IWin
    {
        public WindowDisplay windowDisplay = new("Quiz Loggin", typeof(ProgramOptions), typeof(ProgramFields));
        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public Type? ProgramOptionsType => typeof(ProgramOptions);
        public Type? ProgramFieldsType => typeof(ProgramFields);

        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

        public DateTime DateBirth { get; private set; }


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
                case ProgramOptions.InputLogin:
                    windowDisplay.AddOrUpdateField("Login", InputterData.InputProperty("Login"));
                    break;
                case ProgramOptions.InputPasword:
                    windowDisplay.AddOrUpdateField("Password", InputterData.InputProperty("Password"));
                    break;
                case ProgramOptions.Enter:
                    Enter();
                    break;
                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        private void Enter()
        {
            if (string.IsNullOrEmpty(windowDisplay.Fields["Login"]))
            {
                WindowsHandler.AddInfoWindow( [ "Введите Логин.", "ПЖ >.<" ]);
            }

            else if (string.IsNullOrEmpty(windowDisplay.Fields["Password"]))
            {
                WindowsHandler.AddInfoWindow( [ "Введите Пароль.", "ПЖ >.<" ]);
            }
            else
            {
                User? orLoadUser = UserLoader.TryGetOrLoadUser(windowDisplay.Fields["Login"]);
                if (orLoadUser != null && orLoadUser.Password == windowDisplay.Fields["Password"])
                {
                    Application.UserNow = orLoadUser;
                    Application.WinStack.Pop();
                    Application.WinStack.Push(WindowsHandler.GetWindow<WinGeneralMenuQuiz>());
                    WindowsHandler.AddInfoWindow(["Успешный Вход!"]);
                }
                else
                {
                    WindowsHandler.AddInfoWindow(
                    [
                        "Не верный Логин или Пароль! ,_,",
                        "Попробуйте Снова."
                    ]);
                }
            }
        }

        public enum ProgramOptions
        {
            InputLogin,
            InputPasword,
            Enter,
            Back,
            CountOptions,
        }

        public enum ProgramFields
        {
            Login,
            Password,
        }
    }
}
