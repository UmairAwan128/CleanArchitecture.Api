namespace CleanArchitecture.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommandResponse
    {
        public DateTime UpdatedAtUtc { get; set; }
        public DeleteUserCommand DeleteUserRequest { get; set; }
    }
}
