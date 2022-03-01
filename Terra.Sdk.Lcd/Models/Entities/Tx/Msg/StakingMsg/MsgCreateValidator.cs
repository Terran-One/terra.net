using Newtonsoft.Json;
using Terra.Sdk.Lcd.Models.Entities.PubKey;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    public class MsgCreateValidator : Msg
    {
        public Description Description { get; set; }
        public CommissionRates Commission { get; set; }
        public int MinSelfDelegation { get; set; }
        public string DelegatorAddress { get; set; }
        public string ValidatorAddress { get; set; }
        [JsonProperty("pubkey")]
        public ValConsPublicKey PubKey { get; set; }
        public Coin Value { get; set; }
    }
}
