// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.WinNotification.WinInfo
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll


#nullable enable
namespace QuizTop.UI.Win.WinNotification
{
    public class WinInfo : IWin
    {
        public WindowDisplay windowDisplay = new(MatrixFormater.GetWindowMatrixChar(["Инфа"], "Info"));

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public Type? ProgramOptionsType => null;

        public Type? ProgramFieldsType => null;

        public void UpdateInfoMsg(string[] InfoMessages)
        {
            windowDisplay.Fields.Clear();
            for (int index = 0; index < InfoMessages.Length; ++index)
                windowDisplay.Fields[(index + 1).ToString()] = InfoMessages[index];
            windowDisplay.UpdateCanvas();
        }

        public int SizeX => windowDisplay.MaxLeft;

        public int SizeY => windowDisplay.MaxTop;

        public void InputHandler()
        {
            Console.ReadKey();
            Console.Clear();
            Application.WinStack.Pop();
        }

        public void Show()
        {
            Console.Clear();
            windowDisplay.Show();
        }
    }
}
