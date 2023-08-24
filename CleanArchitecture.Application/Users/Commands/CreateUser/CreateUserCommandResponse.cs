
namespace CleanArchitecture.Application.Users.Commands.CreateUser
{
    public record CreateUserCommandResponse
    {
        public int Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public CreateUserCommand CreateUserRequest { get; set; }
    }
}
