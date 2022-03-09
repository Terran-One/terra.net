using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MarketMsg
{
    [ProtoContract]
    public class MsgSwapSend : Msg
    {
        [ProtoMember(1, Name = "from_address")] public string FromAddress { get; set; }
        [ProtoMember(2, Name = "to_address")] public string ToAddress { get; set; }
        [ProtoMember(3, Name = "offer_coin")] public Coin OfferCoin { get; set; }
        [ProtoMember(4, Name = "ask_denom")] public string AskDenom { get; set; }
    }
}
