using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg
{
    [ProtoContract]
    public class MsgFundCommunityPool : Msg
    {
        protected override System.Type Type => typeof(MsgFundCommunityPool);

        [ProtoMember(1, Name = "depositor")] public string Depositor { get; set; }
        [ProtoMember(2, Name = "amount")] public List<Coin> Amount { get; set; }
    }
}
