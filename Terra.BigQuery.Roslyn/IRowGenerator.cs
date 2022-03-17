using Google.Cloud.BigQuery.V2;

namespace Terra.BigQuery.Roslyn;

public interface IRowGenerator
{
    BigQueryInsertRow GenerateRow(object value);
}
