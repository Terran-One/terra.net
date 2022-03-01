using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Tx
{
    public class Fee
    {
        public long GasLimit { get; set; }
        public List<Coin> Amount { get; set; }
        public string Payer { get; set; }
        public string Granter { get; set; }
    }
}
