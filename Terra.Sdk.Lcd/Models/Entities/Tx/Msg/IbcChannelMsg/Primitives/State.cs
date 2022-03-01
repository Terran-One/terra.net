namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg.Primitives
{
    public enum State
    {
        UninitializedUnspecified = 0,
        Init = 1,
        TryOpen = 2,
        Open = 3,
        Closed = 4,
        Unrecognized = -1
    }
}