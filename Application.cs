using QuizTop.Data.DataHandlers.QuestionHandler;
using QuizTop.Data.DataHandlers.QuizHandler;
using QuizTop.Data.DataHandlers.UserRecordHandler;
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
            Console.SetWindowSize(80, 40);
            CheckOrCreateDirDataBase();
            WinStack.Push(WindowsHandler.GetWindow<WinStart>());
            QuestionLoader.LoadQuestionDataBase();
            QuizLoader.LoadQuizDataBase();
            
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
