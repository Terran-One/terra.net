using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg
{
    public class MsgSend
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public List<Coin> Amount { get; set; }
    }
}
