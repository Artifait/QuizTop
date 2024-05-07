// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.QuizWin.WinQuizGame
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll


#nullable enable
namespace QuizTop.UI.Win.QuizWin
{
    public class WinQuizGame : IWin
    {
        public WindowDisplay windowDisplay = new("Quiz Loggin", []);

        public int SizeX => this.windowDisplay.MaxLeft;
        public int SizeY => this.windowDisplay.MaxTop;

        public WindowDisplay WindowDisplay
        {
            get => this.windowDisplay;
            set => this.windowDisplay = value;
        }

        public Type? ProgramOptionsType => null;
        public Type? ProgramFieldsType => null;

        public void InputHandler() => throw new NotImplementedException();
        public void Show() => throw new NotImplementedException();
    }
}
