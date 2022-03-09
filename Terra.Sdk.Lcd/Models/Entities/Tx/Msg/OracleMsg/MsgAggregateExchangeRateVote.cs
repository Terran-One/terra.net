using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg
{
    [ProtoContract]
    public class MsgAggregateExchangeRateVote : Msg
    {
        [ProtoMember(1, Name = "exchange_rates")] public List<Coin> ExchangeRates { get; set; }
        [ProtoMember(2, Name = "salt")] public string Salt { get; set; }
        [ProtoMember(3, Name = "validator")] public string Validator { get; set; }
    }
}
