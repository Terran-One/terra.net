using System;
using System.Net.Http;
using Terra.Sdk.Lcd.Api;

namespace Terra.Sdk.Lcd
{
    public class LcdClient
    {
        public BankApi Bank { get; }

        public LcdClient(LcdClientConfig config)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(config.Url);

            Bank = new BankApi(httpClient);
        }
    }
}
