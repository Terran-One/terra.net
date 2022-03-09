using ProtoBuf;

namespace Terra.Sdk.Lcd.Models
{
    /// <summary>
    /// Used for serializing fields of type google.protobuf.Any.
    /// </summary>
    /// <remarks>
    /// <see cref="Value"/> should be set to
    /// </remarks>
    [ProtoContract]
    public sealed class Any
    {
        /// <summary>
        /// Taken from the `@type` field.
        /// </summary>
        [ProtoMember(1, Name = "type_url")]
        public string TypeUrl { get; set; }

        /// <summary>
        /// The base64-encoded protobuf value.
        /// </summary>
        /// <remarks>
        /// You can use <see cref="Terra.Sdk.Lcd.Extensions.ObjectExtensions.EncodeProto{T}"/>
        /// to generate this value.
        /// </remarks>
        [ProtoMember(2, Name = "value")]
        public byte[] Value { get;set; }
    }
}
