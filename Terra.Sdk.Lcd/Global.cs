using System.Net.Http;

namespace Terra.Sdk.Lcd
{
    public static class Context
    {
        private static readonly HttpClient HttpClient = new HttpClient();
    }
}
