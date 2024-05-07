// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.QuizWin.WinPastQuizResults
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using System;

#nullable enable
namespace QuizTop.UI.Win.QuizWin
{
    public class WinPastQuizResults : IWin
    {
        public WindowDisplay windowDisplay = new("Quiz Loggin", Array.Empty<string>());

        public int SizeX => throw new NotImplementedException();
        public int SizeY => throw new NotImplementedException();

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
