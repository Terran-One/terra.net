using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    [ProtoContract]
    public class MsgUpdateClient : Msg
    {
        [ProtoMember(1)] public string ClientId { get; set; }
        [ProtoMember(2)] public JObject Header { get; set; }
        [ProtoMember(3)] public string Signer { get; set; }
    }
}