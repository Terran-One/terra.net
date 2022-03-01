namespace Terra.Sdk.Lcd.Models.Entities.Account
{
    public class BaseAccount : Account
    {
        public string Address { get; set; }

        public PubKey.PubKey PubKey { get; set; }

        public long AccountNumber { get; set; }

        public long Sequence { get; set; }
    }
}
