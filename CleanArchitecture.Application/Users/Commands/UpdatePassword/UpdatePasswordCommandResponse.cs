namespace CleanArchitecture.Application.Users.Commands.UpdatePassword
{
    public record UpdatePasswordCommandResponse
    {
        public DateTime UpdatedAtUtc { get; set; }
        public UpdatePasswordCommand UpdatePasswordRequest { get; set; }
    }
}
