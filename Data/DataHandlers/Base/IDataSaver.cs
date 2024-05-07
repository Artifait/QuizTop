// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.Base.IDataSaver`1
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

#nullable enable
namespace QuizTop.Data.DataHandlers.Base
{
    public interface IDataSaver<T>
    {
        void SaveItemDataBase();

        void SaveItem(T item);

        string GetItemName(T item);

        string GetSearchMaskItem();
    }
}
