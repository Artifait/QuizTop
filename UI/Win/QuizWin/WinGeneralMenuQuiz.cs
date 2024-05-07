// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.QuizWin.WinGeneralMenuQuiz
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.UserRecordHandler;
using QuizTop.Data.DataStruct.UserStruct;
using QuizTop.UI.Win.AdminWin;
using QuizTop.UI.Win.ApplicationWin;

#nullable enable
namespace QuizTop.UI.Win.QuizWin
{
    public class WinGeneralMenuQuiz : IWin
    {
        public WindowDisplay windowDisplay;

        public WinGeneralMenuQuiz()
        {
            windowDisplay = new WindowDisplay("Quiz Top Game", typeof(WinGeneralMenuQuiz.ProgramOptions));
            if (Application.UserNow.Permission == UserPermission.Admin)
                windowDisplay.AddOption("Панель Админа");
            UserRecordLoader.LoadUserRecords(Application.UserNow.UserName);
        }

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

        public Type? ProgramOptionsType => typeof(WinGeneralMenuQuiz.ProgramOptions);
        public Type? ProgramFieldsType => null;

        public void Show() => windowDisplay.Show();
        public void InputHandler()
        {
            char lower = char.ToLower(Console.ReadKey().KeyChar);

            WindowTools.UpdateCursorPos(lower, ref windowDisplay, windowDisplay.Options.Count);

            if (WindowTools.IsKeySelect(lower)) HandlerMetodMenu();
        }

        public void HandlerMetodMenu()
        {
            Console.Clear();
            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.ChooseTheme:
                    Application.WinStack.Push(WindowsHandler.GetWindow<WinChooseTheme>());
                    break;
                case ProgramOptions.Statistic:
                    Application.WinStack.Push(WindowsHandler.GetWindow<WinUserStatistics>());
                    break;
                case ProgramOptions.EditProfile:
                    Application.WinStack.Push(WindowsHandler.GetWindow<WinProfile>());
                    break;
                case ProgramOptions.Exit:
                    Application.IsRunning = false;
                    break;
                default:
                    Application.WinStack.Push(WindowsHandler.GetWindow<WinAdminPanel>());
                    break;
            }
        }

        public enum ProgramOptions
        {
            ChooseTheme,
            Statistic,
            EditProfile,
            Exit,
            CountMethod,
        }
    }
}
