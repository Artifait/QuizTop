// Decompiled with JetBrains decompiler
// Type: QuizTop.Data.DataHandlers.Base.EventBus
// Assembly: QuizTop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1907BD2-9C38-4A4D-AABC-BC06CA653E63
// Assembly location: C:\Users\user\OneDrive\Рабочий стол\net8.0\QuizTop.dll

using System;
using System.Collections.Generic;

#nullable enable
namespace QuizTop.Data.DataHandlers.Base
{
  public static class EventBus
  {
    private static Dictionary<string, List<Action<object>>> eventHandlers = new Dictionary<string, List<Action<object>>>();

    public static void Subscribe(string eventName, Action<object> handler)
    {
      if (!EventBus.eventHandlers.ContainsKey(eventName))
        EventBus.eventHandlers[eventName] = new List<Action<object>>();
      EventBus.eventHandlers[eventName].Add(handler);
    }

    public static void UnSubscribe(string eventName, Action<object> handler)
    {
      if (!EventBus.eventHandlers.ContainsKey(eventName))
        return;
      EventBus.eventHandlers[eventName].Remove(handler);
    }

    public static void Publish(string eventName, object? data = null)
    {
      if (!EventBus.eventHandlers.ContainsKey(eventName))
        return;
      foreach (Action<object> action in EventBus.eventHandlers[eventName])
      {
        if (action != null)
          action(data);
      }
    }
  }
}
