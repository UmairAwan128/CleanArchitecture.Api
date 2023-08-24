using CleanArchitecture.Infrastructure.Models.Get;

namespace CleanArchitecture.Infrastructure.Identity
{
    public interface ISecurityContext
    {
        GetUserModel User { get; }

        bool IsAdministrator { get; }
    }
}