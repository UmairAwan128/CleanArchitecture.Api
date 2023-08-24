using CleanArchitecture.Domain.Entities.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Role : BaseEntity, ISoftDelete
    {
        public required string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
