
namespace APICatalogo.Logging;

public class CustomerLogger : ILogger
{
    private readonly string _loggerName;
    private readonly CustomLoggerProviderConfig _config;

    public CustomerLogger(string loggerName, CustomLoggerProviderConfig config)
    {
        _loggerName = loggerName;
        _config = config;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == _config.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId} - {formatter(state, exception)}";

        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        string caminhoArquivoLog = "";
        StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true);
        try
        {
            streamWriter.WriteLine(mensagem);
            streamWriter.Close();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
