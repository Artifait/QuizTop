// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.ApplicationWin.WinUserStatistics
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.UserRecordHandler;
using QuizTop.Data.DataStruct.QuizStruct;
using QuizTop.Data.DataStruct.UserRecordStruct;


#nullable enable
namespace QuizTop.UI.Win.ApplicationWin
{
    public class WinUserStatistics : IWin
    {
        public WindowDisplay windowDisplay;
        public static int subjectSearch;

        public WinUserStatistics()
        {
            windowDisplay = new WindowDisplay("Quiz Application", ProgramOptionsType, ProgramFieldsType,
                [new("Статистика по " + Enum.GetName((Subject)subjectSearch), [], new Dictionary<string, string>() { { "1", "Нету" } })]);
            windowDisplay.WindowList[0].NumberedOptions = false;
            SetSubject(0);
        }

        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

        public Type? ProgramOptionsType => typeof(ProgramOptions);
        public Type? ProgramFieldsType => typeof(ProgramFields);

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public void Show() => windowDisplay.Show();

        public void InputHandler()
        {
            char lower = char.ToLower(Console.ReadKey().KeyChar);

            WindowTools.UpdateCursorPos(lower, ref windowDisplay, (int)ProgramOptions.CountMethod);

            if (WindowTools.IsKeySelect(lower)) HandlerMetodMenu();
        }

        public void HandlerMetodMenu()
        {
            Console.Clear();
            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.NextSubject:
                    SetSubject(subjectSearch + 1);
                    break;

                case ProgramOptions.PrevSubject:
                    SetSubject(subjectSearch - 1);
                    break;

                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        public void SetSubject(int newSubject)
        {
            subjectSearch = newSubject;
            WindowTools.CircleUpdateCursor(ref subjectSearch, 0, (int)Subject.CountSubject);
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Subject), Enum.GetName((Subject)subjectSearch));


            List<string> titlesRecords = [];
            if((Subject)subjectSearch == Subject.AllEverything)
            {
                var AllRecords = UserRecordDataBase.GetAllRecordsByLogin(Application.UserNow.UserName);
                AllRecords.ForEach(x => titlesRecords.Add($"{x.QuizTitle} - {x.Grade} баллов"));
            }
            else
            {
                if(UserRecordDataBase.Records[Application.UserNow.UserName].TryGetValue((Subject)subjectSearch, out var records))
                {
                    foreach (var item in records)
                        titlesRecords.Add($"{item.Value.QuizTitle} - {item.Value.Grade} баллов");
                }
            }
            if (titlesRecords.Count == 0)
                titlesRecords.Add("Нету Записей.");


            windowDisplay.WindowList[0].Fields.Clear();
            windowDisplay.WindowList[0].Title = "Статистика по " + Enum.GetName((Subject)subjectSearch);
            for (int index = 0; index < titlesRecords.Count; ++index)
                windowDisplay.WindowList[0].AddOrUpdateField(index.ToString(), titlesRecords[index]);


            windowDisplay.WindowList[0].UpdateCanvas();
        }

        public enum ProgramOptions
        {
            NextSubject,
            PrevSubject,
            Back,
            CountMethod,
        }

        public enum ProgramFields
        {
            Subject,
        }
    }
}
