using System.Runtime.Serialization;

namespace Terra.Sdk.Lcd.Models.Entities.BlockInfo
{
    public enum BlockIdFlag
    {
        [EnumMember(Value = "BLOCK_ID_FLAG_UNKNOWN")]
        Unknown = 0,
        [EnumMember(Value = "BLOCK_ID_FLAG_ABSENT")]
        Absent = 1,
        [EnumMember(Value = "BLOCK_ID_FLAG_COMMIT")]
        Commit = 2,
        [EnumMember(Value = "BLOCK_ID_FLAG_NIL")]
        Nil = 3
    }
}