// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.AdminWin.WinCreatorQuiz
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.QuizStruct;
using System;
using System.Runtime.CompilerServices;

#nullable enable
namespace QuizTop.UI.Win.AdminWin
{
  public class WinCreatorQuiz : IWin
  {
    public WindowDisplay windowDisplay;
    public Subject QuizSubject = Subject.Biology;

    public WinCreatorQuiz()
    {
      this.windowDisplay = new WindowDisplay("Creator Quiz", typeof (WinCreatorQuiz.ProgramOptions), typeof (WinCreatorQuiz.ProgramFields));
      this.windowDisplay.AddOrUpdateField(nameof (QuizSubject), Enum.GetName<Subject>(this.QuizSubject));
      this.windowDisplay.AddOrUpdateField("Id", QuizDataBase.InfoQuizDataBase.CountQuiz.ToString());
      this.windowDisplay.AddOrUpdateField("IdOfSubject", QuizDataBase.InfoQuizDataBase.CountQuizOfSubject[this.QuizSubject].ToString());
    }

    public WindowDisplay WindowDisplay
    {
      get => this.windowDisplay;
      set => this.windowDisplay = value;
    }

    public Type? ProgramOptionsType => typeof (WinCreatorQuiz.ProgramOptions);

    public Type? ProgramFieldsType => typeof (WinCreatorQuiz.ProgramFields);

    public int SizeX => this.windowDisplay.MaxLeft;

    public int SizeY => this.windowDisplay.MaxTop;

    public void Show() => this.windowDisplay.Show();

    public void InputHandler()
    {
      char lower = char.ToLower(Console.ReadKey().KeyChar);
      WindowTools.UpdateCursorPos(lower, ref this.windowDisplay, 4);
      if (!WindowTools.IsKeySelect(lower))
        return;
      this.HandlerMetodMenu();
    }

    private void HandlerMetodMenu()
    {
      Console.Clear();
      switch (this.windowDisplay.CursorPosition)
      {
        case 0:
          this.windowDisplay.AddOrUpdateField("Title", InputterData.InputProperty("Title"));
          break;
        case 1:
          this.InputSubject();
          break;
        case 2:
          this.InputQuestionsId();
          break;
        case 3:
          Application.WinStack.Pop();
          break;
      }
    }

    private void InputSubject()
    {
      Array values = Enum.GetValues(typeof (Subject));
      Console.WriteLine("Введите нужный id предмета.");
      for (int index = 0; index < values.Length - 1; ++index)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
        interpolatedStringHandler.AppendFormatted<int>(index);
        interpolatedStringHandler.AppendLiteral(" - ");
        interpolatedStringHandler.AppendFormatted(Enum.GetName(typeof (Subject), (object) index));
        Console.WriteLine(interpolatedStringHandler.ToStringAndClear());
      }
      this.QuizSubject = (Subject) int.Parse(Console.ReadLine());
      this.windowDisplay.AddOrUpdateField("QuizSubject", Enum.GetName<Subject>(this.QuizSubject));
      WindowDisplay windowDisplay1 = this.windowDisplay;
      int countQuiz = QuizDataBase.InfoQuizDataBase.CountQuiz;
      string str1 = countQuiz.ToString();
      windowDisplay1.AddOrUpdateField("Id", str1);
      WindowDisplay windowDisplay2 = this.windowDisplay;
      countQuiz = QuizDataBase.InfoQuizDataBase.CountQuizOfSubject[this.QuizSubject];
      string str2 = countQuiz.ToString();
      windowDisplay2.AddOrUpdateField("IdOfSubject", str2);
    }

    private void InputQuestionsId()
    {
    }

    public enum ProgramOptions
    {
      InputTitle,
      InputSubject,
      InputQuestionsId,
      Back,
      CountOptions,
    }

    public enum ProgramFields
    {
      Title,
      QuizSubject,
      QuestionsId,
      Id,
      IdOfSubject,
    }
  }
}
