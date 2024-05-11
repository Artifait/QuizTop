// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataStruct.QuestionStruct.InfoQuestionDataBase
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using QuizTop.Data.DataStruct.QuizStruct;

#nullable enable
namespace QuizTop.Data.DataStruct.QuestionStruct
{
    [Serializable]
    public class InfoQuestionDataBase
    {
        public Dictionary<Subject, int> CountQuestionsOfSubject { get; set; } = new Dictionary<Subject, int>();
        public int CountQuestions { get; set; } = 0;

        public InfoQuestionDataBase()
        {
            foreach (Subject key in Enum.GetValues(typeof(Subject))) CountQuestionsOfSubject.Add(key, 0);
        }
    }
    public static class QuestionDataBase
    {
        public static Dictionary<Subject, List<Question>> Questions = [];
        public static Dictionary<int, Question> QuestionsById = [];

        public static InfoQuestionDataBase InfoQuestionDataBase { get; set; } = new InfoQuestionDataBase();

        static QuestionDataBase()
        {
            foreach (Subject key in Enum.GetValues(typeof(Subject)))
                Questions.Add(key, []);
        }

        public static List<Question> GetQuestionBySubject(Subject subject)
        {
            List<Question> totalArray = [];
            if(subject == Subject.AllEverything)
            {
                var allQuestionsList = Questions.Values.ToList();
                foreach (var question in allQuestionsList)
                    totalArray.AddRange(question);
            }
            else
            {
                totalArray = Questions[subject];
            }
            return totalArray;
        }
        public static string GetFileNameInfoDateBase() => Application.DataBasePaths[typeof(QuestionDataBase)] + "QuestionDataBaseInfo.json";
    }
}
