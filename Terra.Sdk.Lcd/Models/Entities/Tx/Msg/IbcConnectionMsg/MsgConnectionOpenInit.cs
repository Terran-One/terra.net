using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg.Primitives;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    [ProtoContract]public class MsgConnectionOpenInit : Msg
    {
        [ProtoMember(1)]public string ClientId { get; set; }
        [ProtoMember(2)]public Counterparty Counterparty { get; set; }
        [ProtoMember(3)]public Version Version { get; set; }
        [ProtoMember(4)]public string DelayPeriod { get; set; }
        [ProtoMember(5)]public string Signer { get; set; }
    }
}
