using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg
{
    [ProtoContract]
    public class MsgMultiSend : Msg
    {
        [ProtoMember(1)] public List<Input> Inputs { get; set; }
        [ProtoMember(2)] public List<Output> Outputs { get; set; }

        [ProtoContract]
        public class Input
        {
            [ProtoMember(1)] public string Address { get; set; }
            [ProtoMember(2)] public List<Coin> Coins { get; set; }
        }

        [ProtoContract]
        public class Output
        {
            [ProtoMember(1)] public string Address { get; set; }
            [ProtoMember(2)] public List<Coin> Coins { get; set; }
        }
    }
}