using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg
{
    [ProtoContract]public class MsgDelegateFeedConsent : Msg
    {
        [ProtoMember(1)]public string Operator { get; set; }
        [ProtoMember(2)]public string Delegate { get; set; }
    }
}
