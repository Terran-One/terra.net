using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    [ProtoContract]
    public class MsgCreateClient : Msg
    {
        [ProtoMember(1)] public JObject ClientState { get; set; }
        [ProtoMember(2)] public JObject ConsensusState { get; set; }
        [ProtoMember(3)] public string Signer { get; set; }
    }
}