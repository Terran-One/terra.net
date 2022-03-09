using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MarketMsg
{
    [ProtoContract]
    public class MsgSwap : Msg
    {
        [ProtoMember(1, Name = "trader")] public string Trader { get; set; }
        [ProtoMember(2, Name = "offer_coin")] public Coin OfferCoin { get; set; }
        [ProtoMember(3, Name = "ask_denom")] public string AskDenom { get; set; }
    }
}
