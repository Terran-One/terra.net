using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    public class BasicAllowance : Allowance
    {
        public List<Coin> SpendLimit { get; set; }
        public string Expiration { get; set; }
    }
}
