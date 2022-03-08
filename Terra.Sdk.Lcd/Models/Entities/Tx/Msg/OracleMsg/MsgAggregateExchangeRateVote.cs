using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg
{
    [ProtoContract]
    public class MsgAggregateExchangeRateVote : Msg
    {
        [ProtoMember(1)] public List<Coin> ExchangeRates { get; set; }
        [ProtoMember(2)] public string Salt { get; set; }
        [ProtoMember(3)] public string Validator { get; set; }
    }
}