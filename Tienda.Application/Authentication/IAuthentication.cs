namespace Tienda.Application.Authentication
{
    public interface IAuthentication
    {
        bool Validate(string user, string password);
    }
}
