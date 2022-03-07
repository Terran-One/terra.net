using System.Runtime.Serialization;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    public enum State
    {
        [EnumMember(Value = "STATE_UNINITIALIZED_UNSPECIFIED")]
        UninitializedUnspecified = 0,
        [EnumMember(Value = "STATE_INIT")]
        Init = 1,
        [EnumMember(Value = "STATE_TRYOPEN")]
        TryOpen = 2,
        [EnumMember(Value = "STATE_OPEN")]
        Open = 3,
        [EnumMember(Value = "STATE_CLOSED")]
        Closed = 4,
        [EnumMember(Value = "UNRECOGNIZED")]
        Unrecognized = -1
    }
}
