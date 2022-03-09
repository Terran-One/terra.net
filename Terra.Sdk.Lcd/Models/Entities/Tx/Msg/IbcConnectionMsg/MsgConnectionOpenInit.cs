using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg
{
    [ProtoContract]
    public class MsgConnectionOpenInit : Msg
    {
        [ProtoMember(1, Name = "client_it")] public string ClientId { get; set; }
        [ProtoMember(2, Name = "counterparty")] public Counterparty Counterparty { get; set; }
        [ProtoMember(3, Name = "version")] public Version Version { get; set; }
        [ProtoMember(4, Name = "delay_period")] public string DelayPeriod { get; set; }
        [ProtoMember(5, Name = "signer")] public string Signer { get; set; }
    }
}
