using System.Text;

namespace Terra.BigQuery.Etl;

public readonly struct AppArgs
{
    public AppArgs()
    {
        Command = default;
        Host = default;
        Db = default;
        BatchSize = default;

        _error = new Lazy<StringBuilder>();
    }

    private AppArgs(string command, string host, string db, int? batchSize)
    {
        Command = command;
        Host = host;
        Db = db;
        BatchSize = batchSize;

        _error = new Lazy<StringBuilder>();
    }

    public string Command { get; }
    public string Host { get; }
    public string Db { get; }
    public int? BatchSize { get; }

    public string Error => _error.Value.Length == 0 ? null : _error.Value.ToString().TrimEnd(Environment.NewLine.ToCharArray());
    private readonly Lazy<StringBuilder> _error;

    public static AppArgs FromCommandLineArgs(string[] args)
    {
        if (args.Length < 3)
        {
            var appArgs = new AppArgs();
            appArgs.Usage();
            return appArgs;
        }

        var command = args[0];
        var host = args[1];
        var db = args[2];
        var options = args.Skip(3).ToArray();

        int? batchSize = null;
        for (var i = 0; i <= options.Length - 1; i += 2)
        {
            switch (options[i])
            {
                case "-b":
                case "--batch-size":
                    if (i == options.Length - 1)
                    {
                        var appArgs = new AppArgs();
                        appArgs.Usage();
                        appArgs.ArgRequired(options[i]);
                        return appArgs;
                    }

                    if (!int.TryParse(options[i + 1], out var b))
                    {
                        var appArgs = new AppArgs();
                        appArgs.Usage();
                        appArgs.InvalidArg(options[i],options[i + 1]);
                        return appArgs;
                    }

                    batchSize = b;
                    break;
                default:
                    {
                        var appArgs = new AppArgs();
                        appArgs.Usage();
                        appArgs.UnsupportedOption(options[i]);
                        return appArgs;
                    }
            }
        }

        return new AppArgs(command, host, db, batchSize);
    }

    private void Usage()
    {
        var error = _error.Value;
        error.AppendLine("Usage:");
        error.AppendLine("  dotnet run create|insert DB_HOST BQ_DB [OPTIONS]");
        error.AppendLine();
    }

    private void ArgRequired(string option)
    {
        _error.Value.AppendLine($"Argument required for option {option}");
        _error.Value.AppendLine();
    }

    private void UnsupportedOption(string option)
    {
        _error.Value.AppendLine($"Unsupported option: {option}");
        _error.Value.AppendLine();
    }

    private void InvalidArg(string option, string arg)
    {
        _error.Value.AppendLine($"Invalid argument for option {option}: {arg}");
        _error.Value.AppendLine();
    }
}
