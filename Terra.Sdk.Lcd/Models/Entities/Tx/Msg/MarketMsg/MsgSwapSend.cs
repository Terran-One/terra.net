using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MarketMsg
{
    [ProtoContract]
    public class MsgSwapSend : Msg
    {
        [ProtoMember(1)] public string FromAddress { get; set; }
        [ProtoMember(2)] public string ToAddress { get; set; }
        [ProtoMember(3)] public Coin OfferCoin { get; set; }
        [ProtoMember(4)] public string AskDenom { get; set; }
    }
}