// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.AdminWin.WinCreatorQuiz
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.QuizHandler;
using QuizTop.Data.DataStruct.QuestionStruct;
using QuizTop.Data.DataStruct.QuizStruct;
using System;
using System.Runtime.CompilerServices;

#nullable enable
namespace QuizTop.UI.Win.AdminWin
{
    public class WinCreatorQuiz : IWin
    {
        public WindowDisplay windowDisplay;
        public Quiz quizOut = new();
        public List<Question> questionsTheme = [];
        public WinCreatorQuiz()
        {
            windowDisplay = new WindowDisplay("Creator Quiz", typeof(ProgramOptions), typeof(ProgramFields));

            windowDisplay.WindowList.Add(new("Question Data Base", []));
            windowDisplay.WindowList.Add(new("Question In Test", []));

            UpdateId();
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.QuizSubject),   Enum.GetName(quizOut.quizSubject));
            UpdateListQuestions();

        }

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public Type? ProgramOptionsType => typeof(ProgramOptions);
        public Type? ProgramFieldsType => typeof(ProgramFields);

        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

        public void Show() => windowDisplay.Show();
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
                case ProgramOptions.InputTitle:
                    quizOut.Title = InputterData.InputProperty(nameof(ProgramFields.Title), "СуперВуперТест");
                    windowDisplay.AddOrUpdateField(nameof(ProgramFields.Title), quizOut.Title);
                    break;
                case ProgramOptions.InputSubject:
                    InputSubject();
                    break;
                case ProgramOptions.AdIdQuestion:
                    AdQuestionsId();
                    break;
                case ProgramOptions.DelIdQuestion:
                    DelQuestionsId();
                    break;
                case ProgramOptions.CreateQuiz:
                    CreateQuiz();
                    break;
                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        private void InputSubject()
        {
            Array values = Enum.GetValues(typeof(Subject));
            Console.WriteLine("Введите нужный id предмета.");
            for (int index = 0; index < values.Length - 1; ++index)
                Console.WriteLine($"{index} - {Enum.GetName(typeof(Subject), index)}");
            
            Console.CursorVisible = true;
            quizOut.quizSubject = (Subject)(int.Parse(Console.ReadLine()) % (int)Subject.CountSubject);
            Console.CursorVisible = Application.CursorVisible;

            UpdateListQuestions();
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.QuizSubject),   Enum.GetName(quizOut.quizSubject));
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Id),            QuizDataBase.InfoQuizDataBase.CountQuiz.ToString());
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.IdOfSubject),   QuizDataBase.InfoQuizDataBase.CountQuizOfSubject[quizOut.quizSubject].ToString());
        }

        private void UpdateListQuestions()
        {
            questionsTheme = QuestionDataBase.GetQuestionBySubject(quizOut.quizSubject);
            windowDisplay.WindowList[0].Fields.Clear();
            windowDisplay.WindowList[0].Title = $"Question Data Base from {quizOut.quizSubject}";

            for (int i = 0; i < questionsTheme.Count; i++)
                windowDisplay.WindowList[0].AddOrUpdateField($"{questionsTheme[i].IdQuestion}", questionsTheme[i].QuestionText);
        }
        private void AdQuestionsId()
        {
            windowDisplay.WindowList[0].Show(false);
            Console.WriteLine("Введите нужный id вопроса.");
            int idNeed = int.Parse(Console.ReadLine());
            if(!QuestionDataBase.QuestionsById.ContainsKey(idNeed))
            {
                WindowsHandler.AddInfoWindow(["Вопроса по данному id не найдено."]);
                return;
            }
            if(quizOut.questionIdList.Contains(idNeed))
            {
                WindowsHandler.AddInfoWindow(["Данный вопрос уже был добавлен."]);
                return;
            }
            quizOut.questionIdList.Add(idNeed);
            UpdateQuestionsId();
        }
        private void DelQuestionsId()
        {
            windowDisplay.WindowList[1].Show(false);
            Console.WriteLine("Введите нужный id вопроса.");
            int idNeed = int.Parse(Console.ReadLine());

            if (quizOut.questionIdList.Contains(idNeed))
            {
                quizOut.questionIdList.Remove(idNeed);
                UpdateQuestionsId();
                return;
            }

            WindowsHandler.AddInfoWindow(["Данного вопроса нету в списке теста."]);
        }
        private void CreateQuiz()
        {
            if (string.IsNullOrWhiteSpace(quizOut.Title))
            {
                WindowsHandler.AddInfoWindow(["Введите название Теста."]);
                return;
            }
            if (quizOut.questionIdList.Count == 0)
            {
                WindowsHandler.AddInfoWindow(["В тесте должен быть минимум 1 вопрос."]);
                return;
            }

            int id = QuizAppender.AddNewQuiz(quizOut);
            WindowsHandler.AddInfoWindow([$"Создание Теста прошло Успешно!", $"Ему присвоен id: {id}"]);
            quizOut = new();
            UpdateId();
            windowDisplay.ClearValuesFields();
            UpdateQuestionsId();
        }
        private void UpdateQuestionsId()
        {
            windowDisplay.WindowList[1].Fields.Clear();
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.QuestionsId), string.Join(", ", quizOut.questionIdList));
            for(int i = 0; i < quizOut.questionIdList.Count; i++)
                windowDisplay.WindowList[1].AddOrUpdateField(i.ToString(), QuestionDataBase.QuestionsById[quizOut.questionIdList[i]].QuestionText);

            windowDisplay.WindowList[1].UpdateCanvas();
        }
        private void UpdateId()
        {
            quizOut.IdQuiz = QuizDataBase.InfoQuizDataBase.CountQuiz;
            quizOut.IdQuizOfSubject = QuizDataBase.InfoQuizDataBase.CountQuizOfSubject[quizOut.quizSubject];

            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Id),            QuizDataBase.InfoQuizDataBase.CountQuiz.ToString());
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.IdOfSubject),   QuizDataBase.InfoQuizDataBase.CountQuizOfSubject[quizOut.quizSubject].ToString());
        }
        public enum ProgramOptions
        {
            InputTitle,
            InputSubject,
            AdIdQuestion,
            DelIdQuestion,
            CreateQuiz,
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
