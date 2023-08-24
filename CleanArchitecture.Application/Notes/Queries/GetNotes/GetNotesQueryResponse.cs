namespace CleanArchitecture.Application.Notes.Queries.GetNotes
{
    public record GetNotesQueryResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? LastModifiedAtUtc { get; set; }
    }
}
