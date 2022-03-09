using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.SlashingMsg
{
    [ProtoContract]
    public class MsgUnjail : Msg
    {
        protected override System.Type Type => typeof(MsgUnjail);

        [ProtoMember(1, Name = "address")] public string Address { get; set; }
    }
}
