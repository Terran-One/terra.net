using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    public class MsgFundCommunityPool : Msg
    {
        public string Depositor { get; set; }
        public List<Coin> Amount { get; set; }
    }
}
