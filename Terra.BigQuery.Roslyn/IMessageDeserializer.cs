using Newtonsoft.Json.Linq;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg;

namespace Terra.BigQuery.Roslyn;

public interface IMessageDeserializer
{
    Tuple<Msg, Type, string> Deserialize(string type, JObject json);
}
