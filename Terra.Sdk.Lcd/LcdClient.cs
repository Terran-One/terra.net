using Terra.Sdk.Lcd.Api;

namespace Terra.Sdk.Lcd
{
    public class LcdClient
    {
        public BankApi Bank { get; }

        public LcdClient(LcdClientConfig config)
        {
            Bank = new BankApi(config.HttpClient);
        }
    }
}
