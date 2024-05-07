// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.Win.ApplicationWin.WinRegistration
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataHandlers.UserHandler;
using QuizTop.Data.DataStruct.UserStruct;
using QuizTop.UI.Win.QuizWin;

#nullable enable
namespace QuizTop.UI.Win.ApplicationWin
{
    public class WinRegistration : IWin
    {
        public WindowDisplay windowDisplay = new("Quiz Application", typeof(ProgramOptions), typeof(ProgramFields));
        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public Type? ProgramOptionsType => typeof(ProgramOptions);
        public Type? ProgramFieldsType => typeof(ProgramFields);

        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;


        public DateTime DateBirth { get; private set; } = DateTime.Now;

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
                case ProgramOptions.InputLogin:
                    windowDisplay.AddOrUpdateField("Login", InputterData.InputProperty("Login"));
                    break;
                case ProgramOptions.InputPasword:
                    windowDisplay.AddOrUpdateField("Password", InputterData.InputProperty("Password"));
                    break;
                case ProgramOptions.InputDate:
                    InputDataBirth();
                    break;
                case ProgramOptions.Enter:
                    Enter();
                    break;
                case ProgramOptions.Back:
                    Application.WinStack.Pop();
                    break;
            }
        }

        private void InputDataBirth()
        {
            DateBirth = InputterData.InputDateTimeHandlerErroreWin("Днюха", out bool _);
            windowDisplay.AddOrUpdateField("Data", DateBirth.ToShortDateString());
        }

        private void Enter()
        {
            string login = windowDisplay.Fields["Login"];
            string password = windowDisplay.Fields["Password"];

            if (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login))
                WindowsHandler.AddInfoWindow(["Введите Логин.", "ПЖ >.<"]);

            else if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
                WindowsHandler.AddInfoWindow(["Введите Пароль.", "ПЖ >.<"]);

            else if (UserLoader.TryGetOrLoadUser(login) != null)
            {
                WindowsHandler.AddInfoWindow(
                [
                  "User с таким именем существует.",
                  "Придумай другой :->"
                ]);
            }
            else
            {

                UserPermission userPermission = !login.Contains("friday", StringComparison.CurrentCultureIgnoreCase) ? UserPermission.User : UserPermission.Admin;
                User user = new()
                {
                    DataBirth = DateBirth,
                    Password = password,
                    UserName = login,
                    Permission = userPermission
                };
                Application.UserNow = user;
                UserDataBase.AddNewUser(user);
                Application.WinStack.Push(WindowsHandler.GetWindow<WinGeneralMenuQuiz>());
                if (userPermission == UserPermission.User)
                {
                    WindowsHandler.AddInfoWindow(
                    [
                        "Вы Успешно зарегистрировались!",
                        "Поставте >12<",
                        "        (:\\/)"
                    ]);
                }
                else
                {
                    WindowsHandler.AddInfoWindow(
                    [
                        "Вы успешно создали новый аккаунт",
                        "Вам выданы права Админа o.O"
                    ]);
                }
                    
            }
        }

        public enum ProgramOptions
        {
            InputLogin,
            InputPasword,
            InputDate,
            Enter,
            Back,
            CountOptions,
        }

        public enum ProgramFields
        {
            Login,
            Password,
            Data,
        }
    }
}
