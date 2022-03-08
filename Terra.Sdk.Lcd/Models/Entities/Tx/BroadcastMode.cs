using System.Runtime.Serialization;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public enum BroadcastMode
    {
        [EnumMember(Value = "BROADCAST_MODE_UNSPECIFIED")]
        Unspecified = 0,

        [EnumMember(Value = "BROADCAST_MODE_BLOCK")]
        Block = 1,

        [EnumMember(Value = "BROADCAST_MODE_SYNC")]
        Sync = 2,

        [EnumMember(Value = "BROADCAST_MODE_ASYNC")]
        Async = 3,
        [EnumMember(Value = "UNRECOGNIZED")] Unrecognized = -1
    }
}