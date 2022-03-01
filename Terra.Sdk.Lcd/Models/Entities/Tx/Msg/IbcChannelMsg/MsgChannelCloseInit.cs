namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg
{
    public class MsgChannelCloseInit : Msg
    {
        public string PortId { get; set; }
        public string ChannelId { get; set; }
        public string Signer { get; set; }
    }
}
