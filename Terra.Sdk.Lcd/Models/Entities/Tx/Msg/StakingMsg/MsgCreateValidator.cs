using Newtonsoft.Json;
using Terra.Sdk.Lcd.Models.Entities.PubKey;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg.Primitives;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg
{
    [ProtoContract]
    public class MsgCreateValidator : Msg
    {
        [ProtoMember(1, Name = "description")] public Description Description { get; set; }
        [ProtoMember(2, Name = "commission")] public CommissionRates Commission { get; set; }
        [ProtoMember(3, Name = "min_self_delegation")] public int MinSelfDelegation { get; set; }
        [ProtoMember(4, Name = "delegator_address")] public string DelegatorAddress { get; set; }
        [ProtoMember(5, Name = "validator_address")] public string ValidatorAddress { get; set; }

        [JsonProperty("pubkey")]
        [ProtoMember(6, Name = "pub_key")]
        public ValConsPublicKey PubKey { get; set; }

        [ProtoMember(7, Name = "value")] public Coin Value { get; set; }
    }
}
