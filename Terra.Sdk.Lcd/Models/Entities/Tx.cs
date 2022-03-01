using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Models.Messages;

namespace Terra.Sdk.Lcd.Models.Entities
{
    public class Tx
    {
        [JsonProperty("body")]
        public TxBody Body { get; set; }

        [JsonProperty("auth_info")]
        public AuthInfo AuthInfo { get; set; }

        [JsonProperty("signatures")]
        public List<string> Signatures { get; set; }
    }

    public class TxBody
    {
        [JsonProperty("messages")]
        public List<Msg> Messages { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }

        [JsonProperty("timeout_height")]
        public long TimeoutHeight { get; set; }
    }

    public class AuthInfo
    {
        [JsonProperty("signer_infos")]
        public List<SignerInfo> SignerInfos { get; set; }

        [JsonProperty("fee")]
        public Fee Fee { get; set; }
    }

    public class SignerInfo
    {
        [JsonProperty("public_key")]
        public PublicKey PublicKey { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("mode_info")]
        public ModeInfo ModeInfo { get; set; }
    }

    public class ModeInfo
    {
        [JsonProperty("single")]
        public SingleMode Single { get; set; }

        [JsonProperty("multi")]
        public MultiMode Multi { get; set; }

        public class SingleMode
        {
            [JsonProperty("mode")]
            public ModeInfo Mode { get; set; }
        }

        public class MultiMode
        {
            [JsonProperty("bitarray")]
            public CompactBitArray BitArray { get; set; }

            [JsonProperty("modeInfos")]
            public List<ModeInfo> ModeInfos { get; set; }
        }

        public enum SignMode
        {
            Unspecified = 0,
            Direct = 1,
            Textual = 2,
            LegacyAminoJson = 127,
            Unrecognized = -1
        }
    }

    public class CompactBitArray
    {
        [JsonProperty("extra_bits_stored")]
        public long ExtraBitsStored { get; set; }

        [JsonProperty("elems")]
        public string Elems { get; set; }
    }

    public class TxInfo
    {
        
    }

    public class Fee
    {
        
    }

    public class BlockTxBroadcastResult
    {
        
    }

    public class SyncTxBroadcastResult
    {
        
    }

    public class AsyncTxBroadcastResult
    {
        
    }

    public class TxSearchResult
    {
        
    }
}