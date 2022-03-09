using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    [ProtoContract]
    public class MsgUpgradeClient : Msg
    {
        protected override System.Type Type => typeof(MsgUpgradeClient);

        [ProtoMember(1, Name = "client_id")] public string ClientId { get; set; }
        [ProtoMember(2, Name = "client_state")] public JObject ClientState { get; set; }
        [ProtoMember(3, Name = "consensus_state")] public JObject ConsensusState { get; set; }
        [ProtoMember(4, Name = "proof_upgrade_client")] public string ProofUpgradeClient { get; set; }
        [ProtoMember(5, Name = "proof_upgrade_consensus_state")] public string ProofUpgradeConsensusState { get; set; }
        [ProtoMember(6, Name = "signer")] public string Signer { get; set; }
    }
}
