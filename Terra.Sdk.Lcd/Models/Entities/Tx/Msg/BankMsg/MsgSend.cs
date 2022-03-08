using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg
{
    [ProtoContract]
    public class MsgSend
    {
        [ProtoMember(1)] public string FromAddress { get; set; }
        [ProtoMember(2)] public string ToAddress { get; set; }
        [ProtoMember(3)] public List<Coin> Amount { get; set; }
    }
}