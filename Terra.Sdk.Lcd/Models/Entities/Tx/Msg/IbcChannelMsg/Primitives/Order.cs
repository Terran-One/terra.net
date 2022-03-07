using System.Runtime.Serialization;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    public enum Order
    {
        [EnumMember(Value = "ORDER_NONE_UNSPECIFIED")]
        NoneUnspecified = 0,
        [EnumMember(Value = "ORDER_UNORDERED")]
        Unordered = 1,
        [EnumMember(Value = "ORDER_ORDERED")]
        Ordered = 2,
        [EnumMember(Value = "UNRECOGNIZED")]
        Unrecognized = -1
    }
}
