// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.AdminWin.WinCreatorQuestion
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.QuestionStruct;
using QuizTop.Data.DataStruct.QuizStruct;
using System.Runtime.CompilerServices;

#nullable enable
namespace QuizTop.UI.Win.AdminWin
{
    internal class WinCreatorQuestion : IWin
    {
        public WindowDisplay windowDisplay;
        public Question quiz;
        public Subject QuestionSubject = Subject.Biology;
        public TypeAnswer QuestionTypeAnswer = TypeAnswer.RadioAnswer;

        public WinCreatorQuestion()
        {
            windowDisplay = new WindowDisplay("Creator Question", typeof(ProgramOptions), typeof(ProgramFields));
            windowDisplay.AddOrUpdateField("Subject", Enum.GetName(QuestionSubject));
            windowDisplay.AddOrUpdateField("TypeAnswer", Enum.GetName(QuestionTypeAnswer));
            windowDisplay.AddOrUpdateField("Id", QuestionDataBase.InfoQuestionDataBase.CountQuestions.ToString());
            windowDisplay.AddOrUpdateField("IdOfSubject", QuestionDataBase.InfoQuestionDataBase.CountQuestionsOfSubject[QuestionSubject].ToString());
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
            WindowTools.UpdateCursorPos(lower, ref windowDisplay, 7);
            if (!WindowTools.IsKeySelect(lower))
                return;
            HandlerMetodMenu();
        }

        private void HandlerMetodMenu()
        {
            Console.Clear();
            switch (windowDisplay.CursorPosition)
            {
                case 0:
                    windowDisplay.AddOrUpdateField("QuestionText", InputterData.InputProperty("QuestionText"));
                    break;
                case 1:
                    InputSubject();
                    break;
                case 2:
                    InputTypeAnswer();
                    break;
                case 3:
                    InputAnswers();
                    break;
                case 4:
                    InputAnswer();
                    break;
                case 5:
                    InputPoints();
                    break;
                case 6:
                    Application.WinStack.Pop();
                    break;
            }
        }

        private void InputSubject()
        {
            Array values = Enum.GetValues(typeof(Subject));
            Console.WriteLine("Введите нужный id предмета.");
            for (int index = 0; index < values.Length - 1; ++index)
            {
                DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
                interpolatedStringHandler.AppendFormatted<int>(index);
                interpolatedStringHandler.AppendLiteral(" - ");
                interpolatedStringHandler.AppendFormatted(Enum.GetName(typeof(Subject), (object)index));
                Console.WriteLine(interpolatedStringHandler.ToStringAndClear());
            }
            QuestionSubject = (Subject)int.Parse(Console.ReadLine());
            windowDisplay.AddOrUpdateField("Subject", Enum.GetName<Subject>(QuestionSubject));
            WindowDisplay windowDisplay1 = windowDisplay;
            int countQuestions = QuestionDataBase.InfoQuestionDataBase.CountQuestions;
            string str1 = countQuestions.ToString();
            windowDisplay1.AddOrUpdateField("Id", str1);
            WindowDisplay windowDisplay2 = windowDisplay;
            countQuestions = QuestionDataBase.InfoQuestionDataBase.CountQuestionsOfSubject[QuestionSubject];
            string str2 = countQuestions.ToString();
            windowDisplay2.AddOrUpdateField("IdOfSubject", str2);
        }

        private void InputTypeAnswer()
        {
            Array values = Enum.GetValues(typeof(TypeAnswer));
            Console.WriteLine("Введите id нужного типа ответа.");
            for (int index = 0; index < values.Length; ++index)
            {
                DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
                interpolatedStringHandler.AppendFormatted<int>(index);
                interpolatedStringHandler.AppendLiteral(" - ");
                interpolatedStringHandler.AppendFormatted(Enum.GetName(ProgramFieldsType, (object)index));
                Console.WriteLine(interpolatedStringHandler.ToStringAndClear());
            }
            QuestionTypeAnswer = (TypeAnswer)int.Parse(Console.ReadLine());
            windowDisplay.AddOrUpdateField("TypeAnswer", Enum.GetName<TypeAnswer>(QuestionTypeAnswer));
        }

        private void InputAnswers()
        {
        }

        private void InputAnswer()
        {
        }

        private void InputPoints()
        {
        }

        public enum ProgramOptions
        {
            InputQuestionText,
            InputSubject,
            InputTypeAnswer,
            InputAnswers,
            InputAnswer,
            InputPoints,
            Back,
            CountOptions,
        }

        public enum ProgramFields
        {
            QuestionText,
            Subject,
            TypeAnswer,
            Answers,
            Answer,
            CountPoints,
            Id,
            IdOfSubject,
        }
    }
}
