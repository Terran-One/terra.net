using System.Collections.Generic;

namespace Terra.Sdk.Lcd.Models.Entities.Deposit.Content
{
    public class ParameterChangeProposal : Content
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ParamChange> Changes { get; set; }
    }
}