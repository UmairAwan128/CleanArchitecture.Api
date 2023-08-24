using CleanArchitecture.Domain.Entities.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Note : BaseEntity, IUpdateAuditEntity, ISoftDelete
    {
        public required string Title { get; set; }
        public string? Content { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatedById { get; set; }
        public virtual User? CreatedBy { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public int? LastModifiedById { get; set; }
        public virtual User? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAtUtc { get; set; }
    }
}
