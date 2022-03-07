using System.Runtime.Serialization;

namespace Terra.Sdk.Lcd.Models.Entities.Staking
{
    public enum BondStatus
    {
        [EnumMember(Value = "BOND_STATUS_UNSPECIFIED")]
        Unspecified = 0,
        [EnumMember(Value = "BOND_STATUS_UNBONDED")]
        Unbonded = 1,
        [EnumMember(Value = "BOND_STATUS_UNBONDING")]
        Unbonding = 2,
        [EnumMember(Value = "BOND_STATUS_BONDED")]
        Bonded = 3,
        [EnumMember(Value = "UNRECOGNIZED")]
        Unrecognized = -1
    }
}
