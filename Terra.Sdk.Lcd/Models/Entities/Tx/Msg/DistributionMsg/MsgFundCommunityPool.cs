using System.Collections.Generic;

using ProtoBuf; namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    [ProtoContract]public class MsgFundCommunityPool : Msg
    {
        [ProtoMember(1)]public string Depositor { get; set; }
        [ProtoMember(2)]public List<Coin> Amount { get; set; }
    }
}
