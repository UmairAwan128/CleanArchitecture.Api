using CleanArchitecture.Application.Models.Get;

namespace CleanArchitecture.Application.Users.Queries.AuthenticateUser
{
    public record AuthenticateUserQueryResponse
    {
        public string? Token { get; set; }
        public GetUserModel? User { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
