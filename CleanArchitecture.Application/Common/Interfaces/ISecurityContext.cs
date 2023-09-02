using CleanArchitecture.Application.Models.Get;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface ISecurityContext
    {
        GetUserModel User { get; }

        bool IsAdministrator { get; }
    }
}