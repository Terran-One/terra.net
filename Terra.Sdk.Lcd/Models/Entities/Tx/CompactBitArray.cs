using System;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    [ProtoContract]
    public class CompactBitArray
    {
        [ProtoMember(1, Name = "extra_bits_stored")] public long ExtraBitsStored { get; set; }
        [ProtoMember(2, Name = "elems")] public byte[] Elems { get; set; }

        public static CompactBitArray FromBits(int bits)
        {
            if (bits <= 0)
                return null;

            var numElems = (bits + 7) / 8;
            if (numElems <= 0 || numElems > Math.Pow(2, 32) - 1)
                // We encountered an overflow here, and shouldn't pass negatives
                // to make, nor should we allow unreasonable limits > maxint32.
                // See https://github.com/cosmos/cosmos-sdk/issues/9162
                return null;

            return new CompactBitArray
            {
                ExtraBitsStored = bits % 8,
                Elems = new byte[numElems]
            };
        }
    }
}
