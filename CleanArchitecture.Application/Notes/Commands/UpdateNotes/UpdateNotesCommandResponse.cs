namespace CleanArchitecture.Application.Notes.Commands.UpdateNote
{
    public record UpdateNoteCommandResponse
    {
        public DateTime UpdatedAtUtc { get; set; }
        public UpdateNoteCommand? UpdateNoteRequest { get; set; }
    }
}
