using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Allowance
{
    public class AllowedMsgAllowance : Allowance
    {
        public Allowance Allowance { get; set; }
        public List<string> AllowedMessages { get; set; }
    }
}
