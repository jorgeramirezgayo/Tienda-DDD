using Microsoft.Extensions.Options;
using Tienda.Application.Helpers;

namespace Tienda.Application.Authentication
{
    public class Authentication : IAuthentication
    {
        private readonly AppSettings _appSettings;

        public Authentication(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public bool Validate(string user, string password)
        {
            // Example of a simple authentication
            return user.ToUpper().Trim() == _appSettings.ApiUser.ToUpper().Trim() &&
                   password == _appSettings.ApiPassword;
        }
    }
}
