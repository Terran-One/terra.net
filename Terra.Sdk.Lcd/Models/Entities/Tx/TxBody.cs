using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class TxBody
    {
        [ProtoMember(1)]
        public List<Msg.Msg> Messages { get; set; }

        [ProtoMember(2)]
        public string Memo { get; set; }

        [ProtoMember(3)]
        public long TimeoutHeight { get; set; }
    }
}
