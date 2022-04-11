using Terra.BigQuery.Etl;
using Terra.BigQuery.Etl.Test;

if (args.Length < 2)
{
    Console.WriteLine("Usage:");
    Console.WriteLine("  dotnet run create|insert DB_HOST [OFFSET]");
    return;
}

var command = args[0];
var host = args[1];
var offset = args.Length >= 3 ? int.Parse(args[3]) : 0;

switch (command)
{
    case "create":
        await Etl.CreateTable();
        break;

    case "insert":
        await Etl.InsertData(host, offset);
        break;

    case "profile":
        EtlBenchmarks.Run();
        break;

    default:
        Console.WriteLine("Valid commands are create, insert");
        break;
}
