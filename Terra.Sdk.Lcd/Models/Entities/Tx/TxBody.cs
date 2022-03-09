using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class TxBody
    {
        //[ProtoMember(1, Name = "messages")]
        public List<Msg.Msg> Messages { get; set; }

        [ProtoMember(2, Name = "memo")]
        public string Memo { get; set; }

        [ProtoMember(3, Name = "timeout_height")]
        public long TimeoutHeight { get; set; }
    }
}
