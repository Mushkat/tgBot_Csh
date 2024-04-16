using Microsoft.Extensions.Logging;

/// <summary>
/// Class for logger provider object.
/// </summary>
public class FileLoggerProvider : ILoggerProvider
{
    private readonly string _logFilePath;

    public FileLoggerProvider(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(_logFilePath);
    }

    public void Dispose()
    {
    }
}

/// <summary>
/// Class for logger object.
/// </summary>
public class FileLogger : ILogger
{
    private readonly string _logFilePath;
    private readonly object _lock = new object();

    public FileLogger(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        lock (_lock)
        {
            using (var writer = new StreamWriter(_logFilePath, true))
            {
                // Format of log.
                writer.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] [{logLevel}] {formatter(state, exception)}");
            }
        }
    }
}