using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    public class MsgEditValidator : Msg
    {
        public Description Description { get; set; }
        public string ValidatorAddress { get; set; }
        public decimal CommissionRate { get; set; }
        public int MinSelfDelegation { get; set; }
    }
}
