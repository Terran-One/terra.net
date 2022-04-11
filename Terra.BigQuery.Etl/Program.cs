using Terra.BigQuery.Etl;

if (args.Length < 3)
{
    Console.WriteLine("Usage:");
    Console.WriteLine("  dotnet run create|insert DB_HOST BQ_DB [OFFSET] [LIMIT]");
    return;
}

var command = args[0];
var host = args[1];
var db = args[2];
var offset = args.Length >= 4 ? int.Parse(args[3]) : (int?)null;
var limit = args.Length >= 5 ? int.Parse(args[4]) : (int?)null;

switch (command)
{
    case "create":
        await Etl.CreateTable(db);
        break;

    case "insert":
        await Etl.InsertData(host, db, offset, limit);
        break;

    default:
        Console.WriteLine("Valid commands are create, insert");
        break;
}
