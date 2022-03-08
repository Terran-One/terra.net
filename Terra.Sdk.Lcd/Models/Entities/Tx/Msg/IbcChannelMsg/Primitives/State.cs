using System.Runtime.Serialization;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    [ProtoContract]
    public enum State
    {
        [EnumMember(Value = "STATE_UNINITIALIZED_UNSPECIFIED")] [ProtoEnum(Name = "STATE_UNINITIALIZED_UNSPECIFIED")]
        UninitializedUnspecified = 0,

        [EnumMember(Value = "STATE_INIT")] [ProtoEnum(Name = "STATE_INIT")]
        Init = 1,

        [EnumMember(Value = "STATE_TRYOPEN")] [ProtoEnum(Name = "STATE_TRYOPEN")]
        TryOpen = 2,

        [EnumMember(Value = "STATE_OPEN")] [ProtoEnum(Name = "STATE_OPEN")]
        Open = 3,

        [EnumMember(Value = "STATE_CLOSED")] [ProtoEnum(Name = "STATE_CLOSED")]
        Closed = 4,

        [EnumMember(Value = "UNRECOGNIZED")] [ProtoEnum(Name = "UNRECOGNIZED")]
        Unrecognized = -1
    }
}