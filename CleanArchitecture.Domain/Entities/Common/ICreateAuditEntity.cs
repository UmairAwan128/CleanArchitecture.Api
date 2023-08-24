namespace CleanArchitecture.Domain.Entities.Common
{
    public interface ICreateAuditEntity
    {
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
