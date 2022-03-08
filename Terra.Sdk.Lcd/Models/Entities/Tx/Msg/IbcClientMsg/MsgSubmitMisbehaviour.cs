using Newtonsoft.Json.Linq;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg
{
    [ProtoContract]public class MsgSubmitMisbehaviour : Msg
    {
        [ProtoMember(1)]public string ClientId { get; set; }
        [ProtoMember(2)]public JObject Misbehaviour { get; set; }
        [ProtoMember(3)]public string Signer { get; set; }
    }
}
