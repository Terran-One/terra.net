using System.Runtime.Serialization;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    [ProtoContract]public enum Order
    {
        [EnumMember(Value = "ORDER_NONE_UNSPECIFIED")]
        [ProtoEnum(Name = "ORDER_NONE_UNSPECIFIED")]
        NoneUnspecified = 0,
        [EnumMember(Value = "ORDER_UNORDERED")]
        [ProtoEnum(Name = "ORDER_UNORDERED")]
        Unordered = 1,
        [EnumMember(Value = "ORDER_ORDERED")]
        [ProtoEnum(Name = "ORDER_ORDERED")]
        Ordered = 2,
        [EnumMember(Value = "UNRECOGNIZED")]
        [ProtoEnum(Name = "UNRECOGNIZED")]
        Unrecognized = -1
    }
}
