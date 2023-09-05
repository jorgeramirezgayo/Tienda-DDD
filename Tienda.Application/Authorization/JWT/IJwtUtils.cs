namespace Tienda.Application.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(string user);
        public string? ValidateJwtToken(string? token);
    }
}
