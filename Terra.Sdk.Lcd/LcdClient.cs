using Terra.Sdk.Lcd.Models.Entities;

namespace Terra.Sdk.Lcd
{
    public class LcdClient
    {
        public Coin Bank { get; }

        public LcdClient(LcdClientConfig config)
        {
            Bank = new Coin(config.HttpClient);
        }
    }
}
