using JsonSubTypes;
using Newtonsoft.Json;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.BankMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.DistributionMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.FeeGrantMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.GovMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcChannelMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcClientMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcConnectionMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.IbcTransferMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MarketMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.MsgAuthMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.OracleMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.SlashingMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.StakingMsg;
using Terra.Sdk.Lcd.Models.Entities.Tx.Msg.WasmMsg;

namespace Terra.Sdk.Lcd.Models.Entities.Tx.Msg
{
    [JsonConverter(typeof(JsonSubtypes), "@type")]
    [JsonSubtypes.KnownSubType(typeof(MsgSend), "/cosmos.bank.v1beta1.MsgSend")]
    [JsonSubtypes.KnownSubType(typeof(MsgMultiSend), "/cosmos.bank.v1beta1.MsgMultiSend")]
    [JsonSubtypes.KnownSubType(typeof(MsgFundCommunityPool), "/cosmos.distribution.v1beta1.MsgFundCommunityPool")]
    [JsonSubtypes.KnownSubType(typeof(MsgSetWithdrawAddress), "/cosmos.distribution.v1beta1.MsgSetWithdrawAddress")]
    [JsonSubtypes.KnownSubType(typeof(MsgWithdrawDelegatorReward), "/cosmos.distribution.v1beta1.MsgWithdrawDelegatorReward")]
    [JsonSubtypes.KnownSubType(typeof(MsgWithdrawValidatorCommission), "/cosmos.distribution.v1beta1.MsgWithdrawValidatorCommission")]
    [JsonSubtypes.KnownSubType(typeof(MsgGrantAllowance), "/cosmos.feegrant.v1beta1.MsgGrantAllowance")]
    [JsonSubtypes.KnownSubType(typeof(MsgRevokeAllowance), "/cosmos.feegrant.v1beta1.MsgRevokeAllowance")]
    [JsonSubtypes.KnownSubType(typeof(MsgDeposit), "/cosmos.gov.v1beta1.MsgDeposit")]
    [JsonSubtypes.KnownSubType(typeof(MsgSubmitProposal), "/cosmos.gov.v1beta1.MsgSubmitProposal")]
    [JsonSubtypes.KnownSubType(typeof(MsgVote), "/cosmos.gov.v1beta1.MsgVote")]
    [JsonSubtypes.KnownSubType(typeof(MsgVoteWeighted), "/cosmos.gov.v1beta1.MsgVoteWeighted")]
    [JsonSubtypes.KnownSubType(typeof(MsgAcknowledgement), "/ibc.core.channel.v1.MsgAcknowledgement")]
    [JsonSubtypes.KnownSubType(typeof(MsgChannelCloseConfirm), "/ibc.core.channel.v1.MsgChannelCloseConfirm")]
    [JsonSubtypes.KnownSubType(typeof(MsgChannelCloseInit), "/ibc.core.channel.v1.MsgChannelCloseInit")]
    [JsonSubtypes.KnownSubType(typeof(MsgChannelOpenAck), "/ibc.core.channel.v1.MsgChannelOpenAck")]
    [JsonSubtypes.KnownSubType(typeof(MsgChannelOpenConfirm), "/ibc.core.channel.v1.MsgChannelOpenConfirm")]
    [JsonSubtypes.KnownSubType(typeof(MsgChannelOpenInit), "/ibc.core.channel.v1.MsgChannelOpenInit")]
    [JsonSubtypes.KnownSubType(typeof(MsgChannelOpenTry), "/ibc.core.channel.v1.MsgChannelOpenTry")]
    [JsonSubtypes.KnownSubType(typeof(MsgRecvPacket), "/ibc.core.channel.v1.MsgRecvPacket")]
    [JsonSubtypes.KnownSubType(typeof(MsgTimeout), "/ibc.core.channel.v1.MsgTimeout")]
    [JsonSubtypes.KnownSubType(typeof(MsgTimeoutOnClose), "/ibc.core.channel.v1.MsgTimeoutOnClose")]
    [JsonSubtypes.KnownSubType(typeof(MsgCreateClient), "/ibc.core.client.v1.MsgCreateClient")]
    [JsonSubtypes.KnownSubType(typeof(MsgSubmitMisbehaviour), "/ibc.core.client.v1.MsgSubmitMisbehaviour")]
    [JsonSubtypes.KnownSubType(typeof(MsgUpdateClient), "/ibc.core.client.v1.MsgUpdateClient")]
    [JsonSubtypes.KnownSubType(typeof(MsgUpgradeClient), "/ibc.core.client.v1.MsgUpgradeClient")]
    [JsonSubtypes.KnownSubType(typeof(MsgConnectionOpenAck), "/ibc.core.connection.v1.MsgConnectionOpenAck")]
    [JsonSubtypes.KnownSubType(typeof(MsgConnectionOpenConfirm), "/ibc.core.connection.v1.MsgConnectionOpenConfirm")]
    [JsonSubtypes.KnownSubType(typeof(MsgConnectionOpenInit), "/ibc.core.connection.v1.MsgConnectionOpenInit")]
    [JsonSubtypes.KnownSubType(typeof(MsgConnectionOpenTry), "/ibc.core.connection.v1.MsgConnectionOpenTry")]
    [JsonSubtypes.KnownSubType(typeof(MsgTransfer), "/ibc.applications.transfer.v1.MsgTransfer")]
    [JsonSubtypes.KnownSubType(typeof(MsgSwap), "/cosmos.market.v1beta1.MsgSwap")]
    [JsonSubtypes.KnownSubType(typeof(MsgSwapSend), "/cosmos.market.v1beta1.MsgSwapSend")]
    [JsonSubtypes.KnownSubType(typeof(MsgExecAuthorized), "/cosmos.authz.v1beta1.MsgExecAuthorized")]
    [JsonSubtypes.KnownSubType(typeof(MsgGrantAuthorization), "/cosmos.authz.v1beta1.MsgGrantAuthorization")]
    [JsonSubtypes.KnownSubType(typeof(MsgRevokeAuthorization), "/cosmos.authz.v1beta1.MsgRevokeAuthorization")]
    [JsonSubtypes.KnownSubType(typeof(MsgAggregateExchangeRatePrevote), "/cosmos.oracle.v1beta1.MsgAggregateExchangeRatePrevote")]
    [JsonSubtypes.KnownSubType(typeof(MsgAggregateExchangeRateVote), "/cosmos.oracle.v1beta1.MsgAggregateExchangeRateVote")]
    [JsonSubtypes.KnownSubType(typeof(MsgDelegateFeedConsent), "/cosmos.oracle.v1beta1.MsgDelegateFeedConsent")]
    [JsonSubtypes.KnownSubType(typeof(MsgUnjail), "/cosmos.slashing.v1beta1.MsgUnjail")]
    [JsonSubtypes.KnownSubType(typeof(MsgBeginRedelegate), "/cosmos.staking.v1beta1.MsgBeginRedelegate")]
    [JsonSubtypes.KnownSubType(typeof(MsgCreateValidator), "/cosmos.staking.v1beta1.MsgCreateValidator")]
    [JsonSubtypes.KnownSubType(typeof(MsgDelegate), "/cosmos.staking.v1beta1.MsgDelegate")]
    [JsonSubtypes.KnownSubType(typeof(MsgEditValidator), "/cosmos.staking.v1beta1.MsgEditValidator")]
    [JsonSubtypes.KnownSubType(typeof(MsgUndelegate), "/cosmos.staking.v1beta1.MsgUndelegate")]
    [JsonSubtypes.KnownSubType(typeof(MsgClearContractAdmin), "/cosmos.wasm.v1beta1.MsgClearContractAdmin")]
    [JsonSubtypes.KnownSubType(typeof(MsgExecuteContract), "/cosmos.wasm.v1beta1.MsgExecuteContract")]
    [JsonSubtypes.KnownSubType(typeof(MsgInstantiateContract), "/cosmos.wasm.v1beta1.MsgInstantiateContract")]
    [JsonSubtypes.KnownSubType(typeof(MsgMigrateCode), "/cosmos.wasm.v1beta1.MsgMigrateCode")]
    [JsonSubtypes.KnownSubType(typeof(MsgMigrateContract), "/cosmos.wasm.v1beta1.MsgMigrateContract")]
    [JsonSubtypes.KnownSubType(typeof(MsgStoreCode), "/cosmos.wasm.v1beta1.MsgStoreCode")]
    [JsonSubtypes.KnownSubType(typeof(MsgUpdateContractAdmin), "/cosmos.wasm.v1beta1.MsgUpdateContractAdmin")]
    public abstract class Msg
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
    }
}
