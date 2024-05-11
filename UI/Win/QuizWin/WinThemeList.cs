// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.QuizWin.WinThemeList
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.UserRecordHandler;
using QuizTop.Data.DataStruct.QuizStruct;
using QuizTop.Data.DataStruct.UserRecordStruct;

#nullable enable
namespace QuizTop.UI.Win.QuizWin
{
    public class WinThemeList : IWin
    {
        public WindowDisplay windowDisplay;
        public Subject subjectSearch;
        private int PageNow;
        private List<Quiz> quizzesNowTheme;
        private Quiz? _QuizNow;
        
        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

        public Type? ProgramOptionsType => typeof(WinThemeList.ProgramOptions);
        public Type? ProgramFieldsType => typeof(WinThemeList.ProgramFields);

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public WinThemeList ChooseTheme(Subject theme)
        {
            subjectSearch = theme;
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Subject), Enum.GetName(subjectSearch));
            SetPage(0);
            return this;
        }

        public void SetPage(int newPage)
        {
            if (newPage != 0 && (newPage < 0 || newPage >= quizzesNowTheme.Count))
            {
                WindowsHandler.AddInfoWindow(["Нет такой Страницы!"]);
            }
            else
            {
                PageNow = newPage;
                windowDisplay.AddOrUpdateField(nameof(ProgramFields.Page), PageNow.ToString());
                quizzesNowTheme = QuizDataBase.GetQuizFromPage(PageNow, 5, subjectSearch);
                List<string> titlesQuiz = [];
  
                windowDisplay.WindowList[0].Fields.Clear();

                foreach (Quiz quiz in quizzesNowTheme)
                    windowDisplay.WindowList[0].AddOrUpdateField(quiz.IdQuiz.ToString(), quiz.Title);

                if (windowDisplay.WindowList[0].Fields.Count == 0)
                    windowDisplay.WindowList[0].AddOrUpdateField(0.ToString(), "НЕТУ ТЕСТОВ");

                windowDisplay.WindowList[0].UpdateCanvas();
            }
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
                case ProgramOptions.NextPage:
                    SetPage(PageNow + 1);
                    break;
                case ProgramOptions.PrevPage:
                    SetPage(PageNow - 1);
                    break;
                case ProgramOptions.ShowQuizTop:
                    if (_QuizNow == null)
                    {
                        WindowsHandler.AddInfoWindow([$"Чтоб посмотреть статиcтику по тесту.\n Введите его id."]);
                        break;
                    }
                    UserRecordLoader.LoadQuizUserRecords(_QuizNow.IdQuiz);
                    List<UserRecord> userRecords = UserRecordDataBase.GetAllRecordsByIdQuiz(_QuizNow.IdQuiz);
                    List<(int, string)> msgRecords = [];
                    userRecords.ForEach(x => msgRecords.Add((x.Grade, $"{x.UserName} - {x.Grade} баллов.")));
                    msgRecords.Sort((x, y) => y.Item1.CompareTo(x.Item1));
                    var resultArray = msgRecords.Take(20).Select(item => item.Item2).ToArray();
                    WindowsHandler.AddInfoWindow(resultArray);
                    break;
                case ProgramOptions.InputIdQuiz:
                    windowDisplay.WindowList[0].Show(false);
                    Console.WriteLine("Введите нужный Id теста:");
                    int id = int.Parse(Console.ReadLine());
                    if(!QuizDataBase.QuizsById.TryGetValue(id, out var quiz))
                    {
                        WindowsHandler.AddInfoWindow([$"Теста по id: {id}, не найдено."]);
                        return;
                    }
                    Console.Clear();
                    _QuizNow = quiz;
                    windowDisplay.AddOrUpdateField(nameof(ProgramFields.TitleQuiz), _QuizNow.Title);
                    windowDisplay.AddOrUpdateField(nameof(ProgramFields.IdQuiz), _QuizNow.IdQuiz.ToString());

                    break;
                case ProgramOptions.StartQuiz:
                    if(_QuizNow == null)
                    {
                        WindowsHandler.AddInfoWindow([$"Чтоб начать тест\n Введите его id."]);
                        return;
                    }
                    var winQuiz =  WindowsHandler.GetWindow<WinQuizGame>().ChooseQuiz(_QuizNow);
                    Application.WinStack.Push(winQuiz);
                    break;
                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        public WinThemeList()
        {
            windowDisplay = new WindowDisplay("Quiz Application", ProgramOptionsType, ProgramFieldsType, [new WindowDisplay("Тесты", [], new Dictionary<string, string>() { { "а", "Нету" } })]);
            PageNow = 0;
            quizzesNowTheme = [];
        }

        public enum ProgramOptions
        {
            NextPage,
            PrevPage,
            InputIdQuiz,
            StartQuiz,
            ShowQuizTop,
            Back,
            CountMethod,
        }

        public enum ProgramFields
        {
            Subject,
            Page,
            IdQuiz,
            TitleQuiz
        }
    }
}
