using Newtonsoft.Json;
using ProtoBuf;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg
{
    [ProtoContract]
    public class MsgMigrateContract : Msg
    {
        protected override System.Type Type => typeof(MsgMigrateContract);

        [ProtoMember(1, Name = "admin")]
        public string Admin { get; set; }

        [ProtoMember(2, Name = "contract")]
        public string Contract { get; set; }

        [ProtoMember(3, Name = "new_code_id")]
        public long NewCodeId { get; set; }

        [JsonIgnore]
        [ProtoMember(4, Name = "migrate_msg")]
        public Any ProtoMigrateMsg
        {
            get => _protoMigrateMsg;
            set
            {
                _protoMigrateMsg = value;
                _migrateMsg = value.DecodeDynamic();
            }
        }
        private Any _protoMigrateMsg;

        public dynamic MigrateMsg
        {
            get => _migrateMsg;
            set
            {
                _migrateMsg = value;
                _protoMigrateMsg = new Any {Value = Any.EncodeDynamic(value)};
            }
        }
        private dynamic _migrateMsg;
    }
}
