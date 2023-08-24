namespace CleanArchitecture.Application.Notes.Commands.CreateNote
{
    public record CreateNoteCommandResponse
    {
        public int Id { get; set; }
        public DateTime CreateAtUtc { get; set; }
        public CreateNoteCommand? NoteRequest { get; set; }
    }
}
