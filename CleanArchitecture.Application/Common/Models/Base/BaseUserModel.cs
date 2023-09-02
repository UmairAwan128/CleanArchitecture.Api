using CleanArchitecture.Application.Models.Get;

namespace CleanArchitecture.Application.Models.Base
{
    public class BaseUserModel
    {
        public required string? Email { get; set; }
        public required string? FirstName { get; set; }
        public required string? LastName { get; set; }
        public GetRoleModel? Role { get; set; }
    }
}
