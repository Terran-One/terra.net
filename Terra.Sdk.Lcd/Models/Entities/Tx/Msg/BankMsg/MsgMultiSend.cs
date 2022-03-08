using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg
{
    public class MsgMultiSend : Msg
    {
        public List<Input> Inputs { get; set; }
        public List<Output> Outputs { get; set; }

        public class Input
        {
            public string Address { get; set; }
            public List<Coin> Coins { get; set; }
        }

        public class Output
        {
            public string Address { get; set; }
            public List<Coin> Coins { get; set; }
        }
    }
}
