namespace CleanArchitecture.Application.Notes.Queries.GetNoteById
{
    public record GetNoteByIdQueryResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Content { get; set; }

        public int CreatedById { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public int? LastModifiedById { get; set; }
        public DateTime? LastModifiedAtUtc { get; set; }
    }
}
