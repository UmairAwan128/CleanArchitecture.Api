namespace CleanArchitecture.Infrastructure.Identity.Auth
{
    public interface ITokenBuilder
    {
        string Build(string name, string role, DateTime expireDate);
    }
}