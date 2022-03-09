using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg
{
    [ProtoContract]
    public class MsgSend : Msg
    {
        protected override Type Type => typeof(MsgSend);

        [ProtoMember(1, Name = "from_address")] public string FromAddress { get; set; }
        [ProtoMember(2, Name = "to_address")] public string ToAddress { get; set; }
        [ProtoMember(3, Name = "amount")] public List<Coin> Amount { get; set; }
    }
}
