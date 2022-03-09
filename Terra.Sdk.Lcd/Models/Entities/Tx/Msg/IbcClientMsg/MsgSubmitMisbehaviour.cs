using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    [ProtoContract]
    public class MsgSubmitMisbehaviour : Msg
    {
        [ProtoMember(1, Name = "client_id")] public string ClientId { get; set; }
        [ProtoMember(2, Name = "misbehaviour")] public JObject Misbehaviour { get; set; }
        [ProtoMember(3, Name = "signer")] public string Signer { get; set; }
    }
}
