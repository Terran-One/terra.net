using Terra.BigQuery.Etl;

var appArgs = AppArgs.FromCommandLineArgs(args);
if (appArgs.Error != null)
{
    Console.WriteLine(appArgs.Error);
    Console.WriteLine();
    return;
}

switch (appArgs.Command)
{
    case "create":
        await Etl.CreateTable(appArgs.Db);
        break;

    case "insert":
        await Etl.InsertData(appArgs.Host, appArgs.Db, appArgs.BatchSize, appArgs.MinId, appArgs.MaxId);
        break;

    default:
        Console.WriteLine("Valid commands are create, insert");
        break;
}
