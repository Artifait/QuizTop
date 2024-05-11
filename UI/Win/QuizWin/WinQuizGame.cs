// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.QuizWin.WinQuizGame
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll


#nullable enable
using QuizTop.Data.DataHandlers.UserRecordHandler;
using QuizTop.Data.DataStruct.QuestionStruct;
using QuizTop.Data.DataStruct.QuizStruct;
using QuizTop.Data.DataStruct.UserRecordStruct;
using Timer = System.Timers.Timer;
namespace QuizTop.UI.Win.QuizWin
{
    public class WinQuizGame : IWin
    {
        public WindowDisplay windowDisplay = new("QuizGame", typeof(ProgramOptions), typeof(ProgramFields));

        public WinQuizGame()
        {
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.QuestionNumber), "0");
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.ProgressComplete), "0/0");
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.QuizTitle), "Супер Тест");
            QuizTimer.Elapsed += OnTimedEvent;
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            var spanTime = DateTime.Now - _DateQuizStart;
            string totalStr = $"{spanTime.Hours} час {spanTime.Minutes} мин {spanTime.Seconds} сек";
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.QuizTime), totalStr);
            windowDisplay.UpdateCanvas();
            if(!_InWinInputAnswer) Show();
        }



        private Quiz? _QuizForTest;
        private Question? _QuestionNow;

        private Timer QuizTimer = new(2000.0) { AutoReset = true, Enabled = true};
        private DateTime _DateQuizStart = DateTime.Now;
        object locker = new();
        private bool _InWinInputAnswer = false;

        private int _QuestionNumberNow = -1;
        private Dictionary<int, string> answersUser = [];
        private Dictionary<int, WindowDisplay> displaysForQuestion = [];


        public WinQuizGame ChooseQuiz(Quiz? quiz)
        {
            _QuizForTest = quiz ?? throw new ArgumentNullException("Такого теста нету");
            _DateQuizStart = DateTime.Now;
            QuizTimer.Enabled = true;

            windowDisplay.ClearValuesFields();
            answersUser.Clear();

            windowDisplay.AddOrUpdateField(nameof(ProgramFields.TimeStart), _DateQuizStart.ToLongTimeString());
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.QuizTitle), _QuizForTest.Title);
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.ProgressComplete), $"0/{_QuizForTest.questionIdList.Count}");


            SetQuestionNumber(0);

            return this;
        }
        private void SetQuestionNumber(int newNumber)
        {
            if(_QuestionNumberNow == newNumber || _QuizForTest == null) return;
            
            _QuestionNumberNow = newNumber;
            WindowTools.CircleUpdateCursor(ref _QuestionNumberNow, 0, _QuizForTest.questionIdList.Count);

            _QuestionNow = QuestionDataBase.QuestionsById[_QuestionNumberNow];
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.QuestionNumber), _QuestionNumberNow.ToString());

            if(!displaysForQuestion.ContainsKey(_QuestionNumberNow))
            {
                displaysForQuestion[_QuestionNumberNow] = WindowTools.GetQuestionWindow(QuestionDataBase.QuestionsById[_QuestionNumberNow]);
                displaysForQuestion[_QuestionNumberNow].CursorVisibility = false;
                displaysForQuestion[_QuestionNumberNow].AddOrUpdateField(nameof(InputAnswer), "");
            }
        }
        private void NextQuestion() => SetQuestionNumber(_QuestionNumberNow + 1);
        private void PrevQuestion() => SetQuestionNumber(_QuestionNumberNow - 1);
        private void InputAnswer()
        {
            if (_QuestionNow == null) throw new ArgumentNullException("Нету вопроса Для ответа...");

            displaysForQuestion[_QuestionNumberNow].Show(false);
            
            string inputAnswer = InputterData.InputAnswerOfQuestion(_QuestionNow.typeAnswer);
            displaysForQuestion[_QuestionNumberNow].AddOrUpdateField(nameof(InputAnswer), inputAnswer);

            if (_QuestionNow.typeAnswer != TypeAnswer.InputAnswer)
            {
                var arrayAnswers = InputterData.ConvertStringToIntArray(inputAnswer);
                for(int i = 0; i < arrayAnswers.Count; i++)  arrayAnswers[i]--;
                inputAnswer = string.Join(", ", arrayAnswers);
            }
            answersUser[_QuestionNow.IdQuestion] = inputAnswer;
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.ProgressComplete), $"{answersUser.Count}/{_QuizForTest.questionIdList.Count}");
        }
        public void FinishTest()
        {
            if(_QuizForTest == null)
            {
                Application.WinStack.Pop();
                return;
            }
            
            UserRecord record = new()
            {
                IdQuiz = _QuizForTest.IdQuiz,
                IdQuizSubject = _QuizForTest.IdQuizOfSubject,
                quizSubject = _QuizForTest.quizSubject,
                QuizTitle = _QuizForTest.Title,
                RecordDate = _DateQuizStart,
                RecordTime = DateTime.Now - _DateQuizStart,
                UserName = Application.UserNow.UserName,
                Grade = 0
            };
            foreach(var answer in answersUser)
            {
                var question = QuestionDataBase.QuestionsById[answer.Key];
                string trueAnswer = question.AnswerOfQuestion;

                if (trueAnswer == answer.Value)  record.Grade += question.CountPoints;
            }

            UserRecordAppender.AddNewRecord(record);
            QuizTimer.Enabled = false;
            
            Application.WinStack.Pop();
            WindowsHandler.AddInfoWindow(["Итоги Прохождения Теста.", $"Оценка: {record.Grade}"]);
        }
        public void Show() {
            lock(locker)
            {
                windowDisplay.Show(true, false);
                displaysForQuestion[_QuestionNumberNow].Show(false, false);
            }
        }
        public void InputHandler()
        {
            char lower = char.ToLower(Console.ReadKey().KeyChar);

            WindowTools.UpdateCursorPos(lower, ref windowDisplay, (int)ProgramOptions.CountOptions);

            if (WindowTools.IsKeySelect(lower)) HandlerMetodMenu();
        }

        private void HandlerMetodMenu()
        {
            Console.Clear();
            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.NextQuestion:
                    NextQuestion();
                    break;

                case ProgramOptions.PrevQuestion:
                    PrevQuestion();
                    break;

                case ProgramOptions.InputAnswer:
                    _InWinInputAnswer = true;
                    InputAnswer();
                    _InWinInputAnswer = false;
                    break;
                case ProgramOptions.FinishQuiz:
                    FinishTest();
                    break;
            }
        }

        public enum ProgramOptions
        {
            NextQuestion,
            PrevQuestion,
            InputAnswer,
            FinishQuiz,
            CountOptions,
        }

        public enum ProgramFields
        {
            QuestionNumber,
            QuizTitle,
            ProgressComplete,
            TimeStart,
            QuizTime
        }

        public Type? ProgramOptionsType => typeof(ProgramOptions);
        public Type? ProgramFieldsType => typeof(ProgramFields);

        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }
    }
}
