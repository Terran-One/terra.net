using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    [ProtoContract]
    public class MsgCreateClient : Msg
    {
        [ProtoMember(1, Name = "client_state")] public JObject ClientState { get; set; }
        [ProtoMember(2, Name = "consensus_state")] public JObject ConsensusState { get; set; }
        [ProtoMember(3, Name = "signer")] public string Signer { get; set; }
    }
}
