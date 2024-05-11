// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.QuizWin.WinChooseTheme
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.QuizStruct;

#nullable enable
namespace QuizTop.UI.Win.QuizWin
{
    public class WinChooseTheme : IWin
    {
        public WindowDisplay windowDisplay = new("Quiz Application", typeof(ProgramOptions));

        public Type? ProgramOptionsType => typeof(ProgramOptions);
        public Type? ProgramFieldsType => typeof(ProgramFields);

        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public void Show() => windowDisplay.Show();

        public void InputHandler()
        {
            char lower = char.ToLower(Console.ReadKey().KeyChar);

            WindowTools.UpdateCursorPos(lower, ref windowDisplay, 6);

            if (WindowTools.IsKeySelect(lower)) HandlerMetodMenu();
        }

        public void HandlerMetodMenu()
        {
            Console.Clear();
            if (windowDisplay.CursorPosition == (int)ProgramOptions.Back)
                Application.WinStack.Pop();
            else if (windowDisplay.CursorPosition >= 0 && windowDisplay.CursorPosition < 5)
                Application.WinStack.Push(WindowsHandler.GetWindow<WinThemeList>().ChooseTheme((Subject)windowDisplay.CursorPosition));
            else
                WindowsHandler.AddInfoWindow([ "Я не Знаю Что ЭТО!" ]);
        }

        public enum ProgramOptions
        {
            Biology,
            Geography,
            History,
            Programming,
            AllEverything,
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
