using System;
using System.Collections.Generic;
using Terra.Sdk.Lcd.Extensions;

namespace Terra.Sdk.Lcd.Models
{
    public class Error
    {
        public string Value { get; set; }
        public int? Code { get; set; }
        public string Message { get; set; }
        public List<string> Details { get; set; }

        internal static Error From(string response)
        {
            Console.WriteLine(response);
            if (response.TryParseJson(out Error error))
                return error;

            return new Error {Message = response};
        }
    }
}
