using Newtonsoft.Json.Linq;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    [ProtoContract]public class MsgUpgradeClient : Msg
    {
        [ProtoMember(1)]public string ClientId { get; set; }
        [ProtoMember(2)]public JObject ClientState { get; set; }
        [ProtoMember(3)]public JObject ConsensusState { get; set; }
        [ProtoMember(4)]public string ProofUpgradeClient { get; set; }
        [ProtoMember(5)]public string ProofUpgradeConsensusState { get; set; }
        [ProtoMember(6)]public string Signer { get; set; }
    }
}
