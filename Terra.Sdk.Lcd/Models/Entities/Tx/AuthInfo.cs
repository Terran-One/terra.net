using System.Collections.Generic;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class AuthInfo
    {
        [ProtoMember(1, Name = "signer_infos")] public List<SignerInfo> SignerInfos { get; set; }
        [ProtoMember(2, Name = "fee")] public Fee Fee { get; set; }
    }
}
