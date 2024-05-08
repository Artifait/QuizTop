// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.AdminWin.WinCreatorQuestion
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.QuestionHandler;
using QuizTop.Data.DataStruct.QuestionStruct;
using QuizTop.Data.DataStruct.QuizStruct;

#nullable enable
namespace QuizTop.UI.Win.AdminWin
{
    internal class WinCreatorQuestion : IWin
    {
        public WindowDisplay windowDisplay;
        public Question questionOut;

        public WinCreatorQuestion()
        {
            windowDisplay = new WindowDisplay("Creator Question", typeof(ProgramOptions), typeof(ProgramFields));
            questionOut = new();
            UpdateId();

            windowDisplay.AddOrUpdateField(nameof(ProgramFields.TypeAnswer),    Enum.GetName(questionOut.typeAnswer));
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Subject),       Enum.GetName(questionOut.questionTypes));
            
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
                case ProgramOptions.InputQuestionText:
                    string input = InputterData.InputProperty("QuestionText", "Whaaat?");
                    questionOut.QuestionText = input;
                    windowDisplay.AddOrUpdateField("QuestionText", input);
                    break;

                case ProgramOptions.InputSubject:
                    InputSubject();
                    break;

                case ProgramOptions.InputTypeAnswer:
                    InputTypeAnswer();
                    break;

                case ProgramOptions.InputVariantAnswer:
                    InputVariantAnswer();
                    break;

                case ProgramOptions.InputAnswer:
                    InputAnswer();
                    break;

                case ProgramOptions.InputPoints:
                    InputPoints();
                    break;

                case ProgramOptions.CreateQuestion:
                    CreateQuestion();
                    break;
                
                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        private void InputSubject()
        {
            int countSubject = (int)Subject.CountSubject;
            Console.WriteLine("Введите нужный id предмета.");
            for (int i = 0; i < countSubject; ++i)
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Subject), i)}");
            
            questionOut.questionTypes = (Subject)(int.Parse(Console.ReadLine()) % (int)Subject.CountSubject);


            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Subject),       Enum.GetName(questionOut.questionTypes));
            UpdateId();
        }

        private void InputTypeAnswer()
        {
            int countTypes = Enum.GetValues(typeof(TypeAnswer)).Length;
            Console.WriteLine("Введите id нужного типа ответа.");
            for (int i = 0; i < countTypes; ++i)
                Console.WriteLine($"{i} - {Enum.GetName(typeof(TypeAnswer), i)}");

            Console.CursorVisible = true;
            questionOut.typeAnswer = (TypeAnswer)(int.Parse(Console.ReadLine()) % countTypes);
            Console.CursorVisible = Application.CursorVisible;

            windowDisplay.AddOrUpdateField(nameof(ProgramFields.TypeAnswer), Enum.GetName(questionOut.typeAnswer));
        }

        private void InputAnswer()
        {
            Console.CursorVisible = true;

            questionOut.AnswerOfQuestion = InputterData.InputAnswerOfQuestion(questionOut.typeAnswer); 
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Answer), questionOut.AnswerOfQuestion);
            Console.CursorVisible = Application.CursorVisible;
        }

        private void InputVariantAnswer()
        {
            Console.WriteLine("Через точку с запятой введите варианты ответов.\n Пример: почки и листья; стебель с листьями и почками; цветок");

            Console.CursorVisible = true;
            string? inputStr = Console.ReadLine();
            Console.CursorVisible = Application.CursorVisible;

            if (IsNormString(inputStr))
            {
                string[] strings = inputStr.Split(';').Select(element => element.Trim()).ToArray();
                for (int i = 0; i < strings.Length; i++)
                {
                    questionOut.AnswerVariants[i] = strings[i];
                }
                windowDisplay.AddOrUpdateField(nameof(ProgramFields.VariantAnswer), string.Join("; ", strings));
            }
        }
        private void InputPoints()
        {
            Console.WriteLine("Введите колво баллов за правильный ответ:");
            int points = int.Parse(Console.ReadLine());
            questionOut.CountPoints = Math.Abs(points);
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.CountPoints), points.ToString());
        }

        public void CreateQuestion()
        {
            if(questionOut.CountPoints == 0)
            {
                WindowsHandler.AddInfoWindow(["Укажите колво баллов выдаваемых за вопрос."]);
                return;
            }
            if(string.IsNullOrWhiteSpace(questionOut.QuestionText))
            {
                WindowsHandler.AddInfoWindow(["Введите текст вопроса."]);
                return;
            }
            if(string.IsNullOrWhiteSpace(questionOut.AnswerOfQuestion))
            {
                WindowsHandler.AddInfoWindow(["Введите ответ на вопрос."]);
                return;
            }
            if (questionOut.typeAnswer != TypeAnswer.InputAnswer )
            {
                if(questionOut.AnswerVariants.Count == 0)
                {
                    WindowsHandler.AddInfoWindow([
                        $"Вы выбрали тип ответа: {Enum.GetName(questionOut.typeAnswer)}",
                        "При данном выборе обязотельно нужно ввести минимум один вариант ответа."
                    ]);
                    return;
                }
                List<int> answers = InputterData.ConvertStringToIntArray(questionOut.AnswerOfQuestion);
                var CanNumberAnswers = questionOut.AnswerVariants.Keys;
                for(int i = 0; i < answers.Count; i++)
                {
                    if (!CanNumberAnswers.Contains(answers[i]))
                    {
                        WindowsHandler.AddInfoWindow([
                            $"В Ответе на вопрос присутствует не допустимое значение.",
                            $"Значение под номером: {i}, со значением: {answers[i]}.",
                            $"Допустимые значения: {CanNumberAnswers.Count} > x >= 0)."
                        ]);
                        return;
                    }
                }
            }
            int id = QuestionsAppender.AddNewQuestion(questionOut);
            WindowsHandler.AddInfoWindow([$"Создание Вопроса прошло Успешно!", $"Ему присвоен id: {id}"]);
            questionOut = new();
            windowDisplay.ClearValuesFields();
            UpdateId();
        }
        public void UpdateId()
        {
            questionOut.IdQuestionOfSubject = QuestionDataBase.InfoQuestionDataBase.CountQuestionsOfSubject[Subject.Biology];
            questionOut.IdQuestion          = QuestionDataBase.InfoQuestionDataBase.CountQuestions;

            windowDisplay.AddOrUpdateField(nameof(ProgramFields.Id),            questionOut.IdQuestion.ToString());
            windowDisplay.AddOrUpdateField(nameof(ProgramFields.IdOfSubject),   questionOut.IdQuestionOfSubject.ToString());
        }
        public bool IsNormString(string? input)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                WindowsHandler.AddInfoWindow(["Ваш Ввод Не Вкусный", "Попробуйте снова."]);
                return false;
            }
            return true;
        }
        public enum ProgramOptions
        {
            InputQuestionText,
            InputSubject,
            InputTypeAnswer,
            InputVariantAnswer,
            InputAnswer,
            InputPoints,
            CreateQuestion,
            Back,
            CountOptions,
        }

        public enum ProgramFields
        {
            QuestionText,
            Subject,
            TypeAnswer,
            VariantAnswer,
            Answer,
            CountPoints,
            Id,
            IdOfSubject,
        }
    }
}
