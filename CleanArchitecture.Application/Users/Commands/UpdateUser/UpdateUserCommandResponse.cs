namespace CleanArchitecture.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommandResponse
    {
        public DateTime UpdatedAtUtc { get; set; }
        public UpdateUserCommand UpdateUserRequest { get; set; }
    }
}
