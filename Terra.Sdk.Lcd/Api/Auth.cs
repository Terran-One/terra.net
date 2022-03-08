using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities.Account;

namespace Terra.Sdk.Lcd.Api
{
    public class Auth
    {
        private readonly LcdClient _client;

        internal Auth(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<Account>> GetAccountInfo(string address) => new Account(_client).Get(address);
    }
}