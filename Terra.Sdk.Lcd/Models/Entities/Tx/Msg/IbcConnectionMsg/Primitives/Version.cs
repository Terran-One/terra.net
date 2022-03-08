using System.Collections.Generic;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg.Primitives
{
    [ProtoContract]
    public class Version
    {
        [ProtoMember(1)]public string Identifier { get; set; }
        [ProtoMember(2)]public List<string> Features { get; set; }
    }
}
