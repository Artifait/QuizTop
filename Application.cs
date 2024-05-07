using QuizTop.Data.DataHandlers.QuestionHandler;
using QuizTop.Data.DataHandlers.QuizHandler;
using QuizTop.Data.DataStruct.QuestionStruct;
using QuizTop.Data.DataStruct.QuizStruct;
using QuizTop.Data.DataStruct.UserRecordStruct;
using QuizTop.Data.DataStruct.UserStruct;
using QuizTop.UI;
using QuizTop.UI.Win.ApplicationWin;

#nullable enable
namespace QuizTop
{
    public static class Application
    {
        public static Stack<IWin> WinStack = [];
        public static bool IsRunning = false;
        public static User UserNow;
        public static string PathData = Directory.GetCurrentDirectory();
        public static Dictionary<Type, string> DataBasePaths = [];

        public static bool CursorVisible { get; set; } = false;

        private static void CheckOrCreateDirDataBase()
        {
            Type[] typeArray = [ typeof (QuestionDataBase), typeof (QuizDataBase), typeof (UserDataBase), typeof (UserRecordDataBase) ];
            foreach (Type key in typeArray)
            {
                string path = Path.Combine(PathData, key.Name);
                DataBasePaths[key] = path + "\\";
                if (!Directory.Exists(path))
                {
                    try { Directory.CreateDirectory(path); }
                    catch (Exception ex) { WindowsHandler.AddErroreWindow([ ex.Message ]); }
                }
            }
        }

        private static void Init()
        {
            Console.Title = "Art Quiz Top";
            CheckOrCreateDirDataBase();
            WinStack.Push(WindowsHandler.GetWindow<WinStart>());
            QuestionLoader.LoadQuestionDataBase();
            QuizLoader.LoadQuestionDataBase();
            Question question = new()
            {
                QuestionText = "Что такое побег?",
                typeAnswer = TypeAnswer.MultiRadioAnswer,
                AnswerVariants = ["часть стебля", "почки и листья", "стебель с листьями и почками", "цветок"],
                AnswerOfQuestion = "стебель с листьями и почками",
                CountPoints = 1,
                questionTypes = Subject.Biology
            };
            int id = QuestionsAppender.AddNewQuestion(question);
            Quiz quiz = new()
            {
                questionIdList = [id],
                quizSubject = Subject.Biology,
                Title = "СуперВуперТестПОБиологии"
            };
            QuizAppender.AddNewQuiz(quiz);
        }

        public static void Run()
        {
            if (IsRunning) return;
            IsRunning = true;
            Init();
            while (IsRunning && WinStack.Count > 0)
            {
                WinStack.Peek().Show();
                Console.CursorVisible = CursorVisible;
                try { WinStack.Peek().InputHandler(); }
                catch (Exception ex) { WindowsHandler.AddErroreWindow([ ex.Message ]); }
            }
        }
    }
}
