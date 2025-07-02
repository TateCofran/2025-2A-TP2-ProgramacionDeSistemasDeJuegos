using System;
using UnityEngine;

public class DebugLogHandler : ILogHandler
{
    private readonly ILogHandler unityHandler;
    private readonly Action<string> logToConsole;

    public DebugLogHandler(Action<string> logToConsole)
    {
        unityHandler = Debug.unityLogger.logHandler; 
        this.logToConsole = logToConsole;
    }
    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        unityHandler.LogFormat(logType, context, format, args);

        var msg = string.Format(format, args);
        logToConsole?.Invoke($"[{logType}] {msg}");
    }

    public void LogException(Exception exception, UnityEngine.Object context)
    {
        unityHandler.LogException(exception, context);
        logToConsole?.Invoke("[Exception] " + exception);
    }
}
