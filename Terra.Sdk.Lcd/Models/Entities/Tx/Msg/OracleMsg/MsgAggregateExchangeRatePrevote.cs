using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg
{
    [ProtoContract]public class MsgAggregateExchangeRatePrevote : Msg
    {
        [ProtoMember(1)]public string Hash { get; set; }
        [ProtoMember(2)]public string Feeder { get; set; }
        [ProtoMember(3)]public string Validator { get; set; }
    }
}
