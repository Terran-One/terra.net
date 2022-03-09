using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg
{
    [ProtoContract]
    public class MsgMultiSend : Msg
    {
        protected override Type Type => typeof(MsgMultiSend);

        [ProtoMember(1, Name = "inputs")] public List<Input> Inputs { get; set; }
        [ProtoMember(2, Name = "outputs")] public List<Output> Outputs { get; set; }

        [ProtoContract]
        public class Input
        {
            [ProtoMember(1, Name = "address")] public string Address { get; set; }
            [ProtoMember(2, Name = "coins")] public List<Coin> Coins { get; set; }
        }

        [ProtoContract]
        public class Output
        {
            [ProtoMember(1, Name = "address")] public string Address { get; set; }
            [ProtoMember(2, Name = "coins")] public List<Coin> Coins { get; set; }
        }
    }
}
