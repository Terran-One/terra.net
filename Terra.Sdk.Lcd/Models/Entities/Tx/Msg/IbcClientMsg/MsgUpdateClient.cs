using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    [ProtoContract]
    public class MsgUpdateClient : Msg
    {
        protected override System.Type Type => typeof(MsgUpdateClient);

        [ProtoMember(1, Name = "client_id")] public string ClientId { get; set; }
        [ProtoMember(2, Name = "header")] public JObject Header { get; set; }
        [ProtoMember(3, Name = "signer")] public string Signer { get; set; }
    }
}
