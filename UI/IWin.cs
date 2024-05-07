// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.IWin
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using System;

#nullable enable
namespace QuizTop.UI
{
    public interface IWin
    {
        Type? ProgramOptionsType { get; }
        Type? ProgramFieldsType { get; }
        WindowDisplay WindowDisplay { get; set; }

        void Show();
        void InputHandler();

        int SizeX { get; }
        int SizeY { get; }
    }
}
