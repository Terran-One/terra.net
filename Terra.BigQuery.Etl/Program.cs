using Newtonsoft.Json;
using Terra.BigQuery.Etl;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg;

void Dump(object value)
{
    var jsonSerializerSettings = new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
    var json = JsonConvert.SerializeObject(value, Formatting.Indented, jsonSerializerSettings);
    Console.WriteLine(json);
}

var schema = Schema.Create<MsgMultiSend>();
Dump(schema);

// var client = BigQueryClient.Create(projectId);
// var schemaOLD = new TableSchemaBuilder
// {
//     { "type", BigQueryDbType.String  },
//     { "value", BigQueryDbType.Struct  }
// }.Build();
//
// var schema = new TableSchema
// {
//     Fields = new List<TableFieldSchema>
//     {
//         new TableFieldSchema {Name = "Type", Type = "STRING", Mode = "REQUIRED"},
//         new TableFieldSchema
//         {
//             Name = "MsgSend",
//             Type = "RECORD",
//             Mode = "NULLABLE",
//             Fields = new List<TableFieldSchema>
//             {
//                 new TableFieldSchema {Name = "FromAddress", Type = "STRING", Mode = "REQUIRED"},
//                 new TableFieldSchema {Name = "ToAddress", Type = "STRING", Mode = "REQUIRED"},
//                 new TableFieldSchema
//                 {
//                     Name = "Coins",
//                     Type = "RECORD",
//                     Mode = "REPEATED",
//                     Fields = new List<TableFieldSchema>
//                     {
//                         new TableFieldSchema {Name = "DENOM", Type = "NUMERIC", Mode = "REQUIRED"}
//                     }
//                 }
//             }
//         }
//     }
// };

// var tableToCreate = new Table
// {
//     TimePartitioning = TimePartition.CreateDailyPartitioning(expiration: null),
//     Schema = schema
// };
// var table = client.CreateTable(datasetId, tableId, tableToCreate);
// // Upload a single row to the table, using JSON rather than the streaming buffer, as
// // the _PARTITIONTIME column will be null while it's being served from the streaming buffer.
// // This code assumes the upload succeeds; normally, you should check the job results.
// table.UploadJson(new[] { "{ \"message\": \"Sample message\" }" }).PollUntilCompleted();
//
// var results = client.ExecuteQuery(
//     $"SELECT message, _PARTITIONTIME AS pt FROM {table}",
//     parameters: null);
// var rows = results.ToList();
// foreach (var row in rows)
// {
//     var message = (string) row["message"];
//     var partition = (DateTime) row["pt"];
//     Console.WriteLine($"Message: {message}; partition: {partition:yyyy-MM-dd}");
// }
