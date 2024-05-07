﻿// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.WindowDisplay
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

#nullable enable
namespace QuizTop.UI
{
    public class WindowDisplay
    {
        public char[,]? CanvasWindow;
        protected int _CursorPosition;
        protected string? _Title;
        protected List<string> _Strings = [];
        protected Dictionary<string, string> _Fields = [];
        public List<WindowDisplay> WindowList = [];
        private bool NeedUpdateCanvas = false;
        private bool _CursorVisibility = true;

        public bool CursorVisibility
        {
            get => _CursorVisibility;
            set{
                if (!value) UpdateCanvas();
                _CursorVisibility = value;
            }
        }

        public int IndexStartCursorPosition { get; set; } = 3;
        public int MaxLeft { get; private set; }
        public int MaxTop { get; private set; }
        public char CursorChar { get; set; } = '*';
        public (int, int) MaxWindowSize => (MaxLeft, MaxTop);

        public event Action UpdateWindowSize;

        public string? Title
        {
            get => _Title;
            set {
                _Title = value;
                NeedUpdateCanvas = true;
            }
        }

        public List<string> Options
        {
            get => _Strings;
            set {
                _Strings = value;
                NeedUpdateCanvas = true;
            }
        }

        public Dictionary<string, string> Fields
        {
            get => _Fields;
            set {
                _Fields = value;
                NeedUpdateCanvas = true;
            }
        }

        public int CursorPosition
        {
            get => _CursorPosition;
            set {
                if (Options != null && Options.Count != 0 && CursorVisibility)
                    CanvasWindow[_CursorPosition + IndexStartCursorPosition, 2] = ' ';
                _CursorPosition = value;
                if (Options == null || Options.Count == 0 || !this.CursorVisibility)
                    return;
                CanvasWindow[_CursorPosition + IndexStartCursorPosition, 2] = CursorChar;
            }
        }

        public void AddOrUpdateField(string key, string value)
        {
            Fields[key] = value;
            NeedUpdateCanvas = true;
        }

        public void AddOption(string option)
        {
            Options.Add(option);
            UpdateCanvas();
        }
        public void ClearValuesFields()
        {
            foreach (var key in Fields.Keys)
                Fields[key] = string.Empty;
            UpdateCanvas();
        }
        public WindowDisplay() : this("ОКНО", ["ОПЦИИ"], new Dictionary<string, string>(){{ "ПОЛЕ", "ЗНАЧЕНИЕ" }}) { }
        public WindowDisplay(char[,] canvasWindow) => CanvasWindow = canvasWindow;
        public WindowDisplay(string title, string[] options) : this(title, options, new Dictionary<string, string>()) { }
        public WindowDisplay(string title, Type options)
        {
            Array values = Enum.GetValues(options);

            for (int index = 0; index < values.Length - 1; ++index)
                Options.Add(Enum.GetName(options, index));

            Title = title;
            CanvasWindow = MatrixFormater.GetWindowMatrixChar(Title, [.. Options], Fields);
        }

        public WindowDisplay(string title, Type options, Type fields, List<WindowDisplay> displays)
          : this(title, options, fields)
        {
            WindowList = displays;
        }

        public WindowDisplay(string title, Type options, Type fields)
        {
            Array values1 = Enum.GetValues(options);
            Array values2 = Enum.GetValues(fields);

            for (int index = 0; index < values1.Length - 1; ++index)
                Options.Add(Enum.GetName(options, index));
            for (int index = 0; index < values2.Length; ++index)
                Fields[Enum.GetName(fields, index)] = "";

            Title = title;
            CanvasWindow = MatrixFormater.GetWindowMatrixChar(Title, [.. Options], Fields);
            CursorPosition = 0;
        }

        public WindowDisplay(string title, string[] strings, Dictionary<string, string> field)
          : this(MatrixFormater.GetWindowMatrixChar(title, strings, field))
        {
            Title = title;

            Options = [.. strings];
            Fields = field;
            CursorPosition = 0;
        }

        public void Show(bool IsMain = true)
        {
            if (NeedUpdateCanvas)
            {
                UpdateCanvas();
                if (IsMain) Console.Clear();
            }
            if (IsMain) Console.SetCursorPosition(0, 0);

            for (int index1 = 0; index1 < CanvasWindow.GetLength(0); ++index1)
            {
                for (int index2 = 0; index2 < CanvasWindow.GetLength(1); ++index2)
                    Console.Write(CanvasWindow[index1, index2]);
                Console.WriteLine();
            }
            Console.WriteLine();
            WindowList.ForEach(x => x.Show(false));

            if (!IsMain) return;
            Console.Write("UserInput: ");
            MaxLeft = Math.Max(CanvasWindow.GetLength(1), WindowList.Count != 0 ? WindowList.Max(x => x.CanvasWindow.GetLength(1)) : 0);
            MaxTop = Console.CursorTop + 2;
        }
        public void UpdateCanvas()
        {
            CanvasWindow = MatrixFormater.GetWindowMatrixChar(Title, [.. Options], Fields);
            CursorPosition = _CursorPosition;
            NeedUpdateCanvas = false;
        }
    }
}
