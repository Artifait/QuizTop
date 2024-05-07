// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.Base.Logger
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#nullable enable
namespace QuizTop.Data.DataHandlers.Base
{
  public class Logger
  {
    private string logFilePath;
    private string iniFilePath;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(
      string section,
      string key,
      string val,
      string filePath);

    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(
      string section,
      string key,
      string def,
      StringBuilder retVal,
      int size,
      string filePath);

    public Logger(string logFilePath, string iniFilePath)
    {
      this.logFilePath = logFilePath;
      this.iniFilePath = iniFilePath;
    }

    public void Log(MessageType messageType, string message)
    {
      string userName = Environment.UserName;
      DateTime now = DateTime.Now;
      string str = this.GetLogEntryFormat().Replace("{date}", now.ToString()).Replace("{type}", messageType.ToString()).Replace("{user}", userName).Replace("{message}", message);
      using (StreamWriter streamWriter = new StreamWriter(this.logFilePath, true))
        streamWriter.WriteLine(str);
    }

    private string GetLogEntryFormat()
    {
      StringBuilder retVal = new StringBuilder((int) byte.MaxValue);
      Logger.GetPrivateProfileString("Settings", "LogEntryFormat", "", retVal, (int) byte.MaxValue, this.iniFilePath);
      return retVal.ToString();
    }
  }
}
