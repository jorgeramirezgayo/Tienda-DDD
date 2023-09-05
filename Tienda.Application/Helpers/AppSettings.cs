namespace Tienda.Application.Helpers
{
    public class AppSettings
    {
        public string JwtSecretKey { get; set; }
        public int JwtExpirationMinutes { get; set; }
        public string ApiUser { get; set; }
        public string ApiPassword { get; set; }
    }
}
