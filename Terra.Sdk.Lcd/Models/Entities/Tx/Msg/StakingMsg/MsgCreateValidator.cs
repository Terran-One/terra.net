using Newtonsoft.Json;
using Terra.Sdk.Lcd.Models.Entities.PubKey;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    [ProtoContract]
    public class MsgCreateValidator : Msg
    {
        [ProtoMember(1)] public Description Description { get; set; }
        [ProtoMember(2)] public CommissionRates Commission { get; set; }
        [ProtoMember(3)] public int MinSelfDelegation { get; set; }
        [ProtoMember(4)] public string DelegatorAddress { get; set; }
        [ProtoMember(5)] public string ValidatorAddress { get; set; }

        [JsonProperty("pubkey")]
        [ProtoMember(6)]
        public ValConsPublicKey PubKey { get; set; }

        [ProtoMember(7)] public Coin Value { get; set; }
    }
}