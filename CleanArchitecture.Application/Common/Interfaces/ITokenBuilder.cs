namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface ITokenBuilder
    {
        string Build(string name, string role, DateTime expireDate);
    }
}