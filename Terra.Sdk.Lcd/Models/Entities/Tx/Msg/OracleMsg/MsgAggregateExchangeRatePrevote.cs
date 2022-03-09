using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg
{
    [ProtoContract]
    public class MsgAggregateExchangeRatePrevote : Msg
    {
        [ProtoMember(1, Name = "hash")] public string Hash { get; set; }
        [ProtoMember(2, Name = "feeder")] public string Feeder { get; set; }
        [ProtoMember(3, Name = "validator")] public string Validator { get; set; }
    }
}
