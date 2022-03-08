using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MarketMsg
{
    [ProtoContract]
    public class MsgSwap : Msg
    {
        [ProtoMember(1)] public string Trader { get; set; }
        [ProtoMember(2)] public Coin OfferCoin { get; set; }
        [ProtoMember(3)] public string AskDenom { get; set; }
    }
}