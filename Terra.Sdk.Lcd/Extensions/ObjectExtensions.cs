using System.Buffers;
using System.IO;
using ProtoBuf;
using ProtoBuf.Meta;

namespace Terra.Sdk.Lcd.Extensions
{
    internal static class ObjectExtensions
    {
        internal static byte[] EncodeProto<T>(this T entity)
        {
            RuntimeTypeModel.Default.InferTagFromNameDefault = true;
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, entity);
                return stream.ToArray();
            }
        }

        internal static T DecodeProto<T>(this byte[] bytes)
        {
            return Serializer.Deserialize<T>(new ReadOnlySequence<byte>(bytes));
        }
    }
}
