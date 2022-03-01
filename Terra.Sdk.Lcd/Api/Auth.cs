using System.Threading.Tasks;
using Terra.Sdk.Lcd.Models;
using Terra.Sdk.Lcd.Models.Entities;
using Terra.Sdk.Lcd.Models.Entities.Account;

namespace Terra.Sdk.Lcd.Api
{
    public class Auth
    {
        private readonly LcdClient _client;

        public Auth(LcdClient client)
        {
            _client = client;
        }

        public Task<Result<Account>> GetAccount(string address) => new Account(_client).Get(address);
    }
}