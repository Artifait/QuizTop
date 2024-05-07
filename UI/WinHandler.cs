// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.WindowsHandler
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.UI.Win.WinNotification;


#nullable enable
namespace QuizTop.UI
{
    public static class WindowsHandler
    {
        public static Dictionary<Type, IWin> WinForms = new Dictionary<Type, IWin>();

        public static T GetWindow<T>() where T : IWin, new()
        {
            if (!WinForms.ContainsKey(typeof(T)))
                WinForms[typeof(T)] = (IWin)new T();
            return (T)WinForms[typeof(T)];
        }

        public static void AddErroreWindow(string[] messages)
        {
            WinErrore window = GetWindow<WinErrore>();

            List<string> stringList1 = [];
            foreach (string message in messages)
                stringList1.AddRange(message.Split('\n'));

            window.UpdateErroreMsg([.. stringList1]);
            Application.WinStack.Push(window);
        }

        public static void AddInfoWindow(string[] messages)
        {
            WinInfo window = GetWindow<WinInfo>();
            List<string> stringList1 = [];
            foreach (string message in messages)
                stringList1.AddRange(message.Split('\n'));

            window.UpdateInfoMsg([.. stringList1]);
            Application.WinStack.Push(window);
        }

        public static string PadCenter(this string str, int totalWidth)
        {
            int count1 = (totalWidth - str.Length) / 2;
            int count2 = totalWidth - str.Length - count1;
            return new string(' ', count1) + str + new string(' ', count2);
        }
    }
}
