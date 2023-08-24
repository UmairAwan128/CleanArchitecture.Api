namespace CleanArchitecture.Domain.Entities.Common
{
    public interface IUpdateAuditEntity : ICreateAuditEntity
    {
        public int? LastModifiedById { get; set; }
        public User? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAtUtc { get; set; }
    }
}
