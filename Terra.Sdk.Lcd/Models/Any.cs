using System.Text;
using Newtonsoft.Json;
using ProtoBuf;
using Terra.Sdk.Lcd.Extensions;

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
        /// You can use <see cref="ProtoExtensions.EncodeProto{T}"/>
        /// to generate this value.
        /// </remarks>
        [ProtoMember(2, Name = "value")]
        public byte[] Value { get;set; }

        /// <summary>
        /// Decodes the instance into a subtype of the given type.
        /// </summary>
        internal T Decode<T>()
        {
            var subtypeMap = typeof(T).GetUrlToTypeMap();
            var decoded = Value.DecodeProto(subtypeMap[TypeUrl]);
            return (T)decoded;
        }

        /// <summary>
        /// Decodes the instance into a subtype of the type dynamic.
        /// </summary>
        internal dynamic DecodeDynamic()
        {
            return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(Value), Global.JsonSerializerSettings);
        }

        internal static byte[] EncodeDynamic(dynamic entity)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(entity, Global.JsonSerializerSettings));
        }
    }
}
