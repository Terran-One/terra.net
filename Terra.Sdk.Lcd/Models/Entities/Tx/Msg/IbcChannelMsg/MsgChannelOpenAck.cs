using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    [ProtoContract]public class MsgChannelOpenAck : Msg
    {
        [ProtoMember(1)]public string PortId { get; set; }
        [ProtoMember(2)]public string ChannelId { get; set; }
        [ProtoMember(3)]public string CounterpartyChannelId { get; set; }
        [ProtoMember(4)]public string CounterpartyVersion { get; set; }
        [ProtoMember(5)]public string ProofTry { get; set; }
        [ProtoMember(6)]public Height ProofHeight { get; set; }
        [ProtoMember(7)]public string Signer { get; set; }
    }
}
