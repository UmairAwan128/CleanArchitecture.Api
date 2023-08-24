namespace CleanArchitecture.Application.Notes.Commands.DeleteNote
{
    public record DeleteNoteCommandResponse
    {
        public DateTime UpdatedAtUtc { get; set; }
        public DeleteNoteCommand DeleteNoteRequest { get; set; }
    }
}
