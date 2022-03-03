using System.Threading.Tasks;
using Terra.Sdk.Lcd.Extensions;
using Terra.Sdk.Lcd.Models.Entities.PubKey;

namespace Terra.Sdk.Lcd.Models.Signing
{
    public abstract class Key
    {
        private readonly PublicKey _publicKey;

        protected Key(PublicKey publicKey)
        {
            _publicKey = publicKey;
        }

        public abstract Task<uint[]> Sign(uint[] payload);

        public string AccAddress => _publicKey.Address;
        public string ValAddress => _publicKey.RawAddress.ToHexString().ConvertToBech32AddressFromHex("terravaloper");
    }
}
