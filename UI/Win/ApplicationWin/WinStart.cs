// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.ApplicationWin.WinStart
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

#nullable enable
namespace QuizTop.UI.Win.ApplicationWin
{
    public class WinStart : IWin
    {
        public WindowDisplay windowDisplay = new("Quiz Application", typeof(ProgramOptions));

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public Type? ProgramOptionsType => typeof(ProgramOptions);
        public Type? ProgramFieldsType => null;

        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

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
                case ProgramOptions.Registration:
                    Application.WinStack.Push(WindowsHandler.GetWindow<WinRegistration>());
                    break;
                case ProgramOptions.Login:
                    Application.WinStack.Push(WindowsHandler.GetWindow<WinLogin>());
                    break;
                case ProgramOptions.Exit:
                    Application.IsRunning = false;
                    break;
            }
        }

        public enum ProgramOptions
        {
            Registration,
            Login,
            Exit,
            CountOptions,
        }
    }
}
