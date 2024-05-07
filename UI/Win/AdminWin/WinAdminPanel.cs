// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.AdminWin.WinAdminPanel
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll


#nullable enable
namespace QuizTop.UI.Win.AdminWin
{
    public class WinAdminPanel : IWin
    {
        public WindowDisplay windowDisplay = new("Admin Panel", typeof(ProgramOptions));

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

            WindowTools.UpdateCursorPos(lower, ref windowDisplay, windowDisplay.Options.Count);

            if (WindowTools.IsKeySelect(lower)) HandlerMetodMenu();
        }

        private void HandlerMetodMenu()
        {
            Console.Clear();
            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.CreateQuestion:
                    Application.WinStack.Push(WindowsHandler.GetWindow<WinCreatorQuestion>());
                    break;
                case ProgramOptions.CreateQuiz:
                    Application.WinStack.Push(WindowsHandler.GetWindow<WinCreatorQuiz>());
                    break;
                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        public enum ProgramOptions
        {
            CreateQuestion,
            CreateQuiz,
            Back,
            CountOptions,
        }
    }
}
