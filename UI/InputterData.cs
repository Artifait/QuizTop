// Decompiled with JetBrains decompiler
// Type: QuizTop.UI.InputterData
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.UserStruct;

#nullable enable
namespace QuizTop.UI
{
    public static class InputterData
    {
        public static Dictionary<string, int> propertyWarnings = new Dictionary<string, int>();

        public static string InputProperty(string property)
        {
            Console.Write("Введите " + property + ": ");
            Console.CursorVisible = true;
            string str = Console.ReadLine();
            Console.CursorVisible = Application.CursorVisible;
            return str;
        }

        public static string InputProperty(string property, string defoaltValue)
        {
            string str = InputProperty(property);
            return string.IsNullOrWhiteSpace(str) ? defoaltValue : str;
        }

        public static string InputPropertyWithWarningWin(string property, out bool Warning)
        {
            string str = InputProperty(property);
            if (!propertyWarnings.ContainsKey(property))
                propertyWarnings[property] = 0;
            if (string.IsNullOrWhiteSpace(str))
            {
                Warning = true;
                WindowsHandler.AddInfoWindow([
                  "ДЛЯ КОГО СКАЗОНО",
                  $"   Введите {property}",
                  "введите пж Ю_Ю."
                ]);
                propertyWarnings[property] = 1;
            }
            else
            {
                Warning = false;
                propertyWarnings[property] = propertyWarnings[property] != 1 ? 0 : -1;
            }
            return str;
        }

        public static DateTime InputDateTime(string TitleDate)
        {
            Console.WriteLine("Введите Дату " + TitleDate + ":\n");
            Console.Write("   ");
            int day = int.Parse(InputProperty("День"));
            Console.Write("   ");
            int month = int.Parse(InputProperty("Месяц"));
            Console.Write("   ");
            return new DateTime(int.Parse(InputProperty("Год")), month, day);
        }

        public static DateTime InputDateTimeHandlerErroreWin(string TitleDate, out bool Warning)
        {
            if (!propertyWarnings.ContainsKey(TitleDate))
                propertyWarnings[TitleDate] = 0;
            DateTime dateTime = DateTime.Now;
            try
            {
                dateTime = InputDateTime(TitleDate);
                if (propertyWarnings[TitleDate] == 1)
                {
                    propertyWarnings[TitleDate] = -1;
                    WindowsHandler.AddInfoWindow([
                        "НЕ так уж и СлОЖнО",
                        "(:\\/)"
                    ]);
                }
                else
                    propertyWarnings[TitleDate] = 0;
                Warning = false;
            }
            catch (Exception ex)
            {
                Warning = true;
                propertyWarnings[TitleDate] = 1;
                WindowsHandler.AddErroreWindow([ ex.Message ]);
            }
            finally
            {
                if (!User.ValidationDateBirth(dateTime))
                {
                    dateTime = DateTime.Now;
                    WindowsHandler.AddInfoWindow([ "ИНVALID ДАННЫЕ!" ]);
                }
            }
            return dateTime;
        }


        public static List<int> ConvertStringToIntArray(string? input)
        {
            if (input == null)
                return [];

            string[] parts = input.Split(',');

            List<int> result = [];

            foreach (string part in parts)
            {
                string numberString = new string(part.Where(c => char.IsDigit(c)).ToArray());

                if (int.TryParse(numberString, out int number))
                    result.Add(number);

            }

            result.Sort();

            return result;
        }
    }
}
