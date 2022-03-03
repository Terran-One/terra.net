using System.Collections.Generic;
using System.Text;
using System.Web;
using Terra.Sdk.Lcd.Models.Entities.Tx;

namespace Terra.Sdk.Lcd.Api.Parameters
{
    public class TxSearchOptions
    {
        public IEnumerable<TxEvent.TxAttribute> Events { get; set; }

        internal string GetQueryString()
        {
            if (Events == null)
                return null;

            var strb = new StringBuilder();
            foreach (var txEvent in Events)
                strb.Append($"&event={HttpUtility.UrlEncode($"{txEvent.Key}={txEvent.Value}")}");

            return strb.ToString().TrimStart('&');
        }
    }
}