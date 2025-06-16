using Zadatak1.Interfaces;

namespace Zadatak1.Services
{
    public class ResponseMessageService : IResponseMessageService
    {
        private readonly IConfiguration _configuration;

        public ResponseMessageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Get(string section, string key)
        {
            return _configuration[$"{section}:{key}"];
        }
    }
}
