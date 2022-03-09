using System.Collections.Generic;
using System.Linq;
using ProtoBuf;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class TxBody
    {
        [ProtoMember(1, Name = "messages")]
        public List<Any> ProtoMessages
        {
            get => _protoMessages;
            set
            {
                _protoMessages = value;
                _messages = value.Select(m => m.Decode<Msg.Msg>()).ToList();
            }
        }
        private List<Any> _protoMessages;

        public List<Msg.Msg> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                _protoMessages = value?.Select(v => new Any
                {
                    TypeUrl = v.Type,
                    Value = v.EncodeProto()
                }).ToList();
            }
        }
        private List<Msg.Msg> _messages;

        [ProtoMember(2, Name = "memo")]
        public string Memo { get; set; }

        [ProtoMember(3, Name = "timeout_height")]
        public long TimeoutHeight { get; set; }
    }
}
