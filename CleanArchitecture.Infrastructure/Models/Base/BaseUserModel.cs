namespace CleanArchitecture.Infrastructure.Models.Get
{
    public class BaseUserModel
    {
        public required string? Email { get; set; }
        public required string? FirstName { get; set; }
        public required string? LastName { get; set; }
        public GetRoleModel? Role { get; set; }
    }
}
