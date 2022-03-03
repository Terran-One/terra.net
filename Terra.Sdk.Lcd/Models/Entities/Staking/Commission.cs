using System;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public class Commission
    {
        public CommissionRates CommissionRates { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}