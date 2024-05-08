// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.QuizWin.WinQuizGame
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll


#nullable enable
using QuizTop.Data.DataStruct.QuestionStruct;
using QuizTop.Data.DataStruct.QuizStruct;
using QuizTop.UI.Win.ApplicationWin;

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
        }
        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        private Quiz? _QuizForTest;
        private Question? _QuestionNow;
        private int _QuestionNumberNow = -1;
        private Dictionary<int, string> answersUser = [];
        private Dictionary<int, WindowDisplay> displaysForQuestion = [];
        public Type? ProgramOptionsType => typeof(ProgramOptions);
        public Type? ProgramFieldsType => typeof(ProgramFields);

        public WinQuizGame ChooseQuiz(Quiz? quiz)
        {
            _QuizForTest = quiz ?? throw new ArgumentNullException("Такого теста нету");
            windowDisplay.ClearValuesFields();
            answersUser.Clear();

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

            
            string inputAnswer = InputterData.InputAnswerOfQuestion(_QuestionNow.typeAnswer);

            displaysForQuestion[_QuestionNumberNow].AddOrUpdateField(nameof(InputAnswer), inputAnswer);
            answersUser[_QuestionNow.IdQuestion] = inputAnswer;
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.ProgressComplete), $"0/{_QuizForTest.questionIdList.Count}");

        }
        public void Show() {
            windowDisplay.Show(true, false);
            displaysForQuestion[_QuestionNumberNow].Show(false, false);
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
            //вместо этого можно было сделать так: при создание windowDisplay мы передаем словарь ключ - ProgramOptions а значение делегат который будет вызываться мдаа
            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.NextQuestion:
                    NextQuestion();
                    break;

                case ProgramOptions.PrevQuestion:
                    PrevQuestion();
                    break;

                case ProgramOptions.InputAnswer:
                    InputAnswer();
                    break;
            }
        }

        public enum ProgramOptions
        {
            NextQuestion,
            PrevQuestion,
            InputAnswer,
            FinishQuiz,
            Back,
            CountOptions,
        }

        public enum ProgramFields
        {
            QuestionNumber,
            QuizTitle,
            ProgressComplete,
        }
    }
}
