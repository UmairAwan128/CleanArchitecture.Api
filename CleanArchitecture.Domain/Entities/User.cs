using CleanArchitecture.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities
{
    public class User : BaseEntity, ISoftDelete
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }
        public required string Password { get; set; }

        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? LastModifiedAtUtc { get; set; }

        public int? CreatedById { get; set; }
        public int? LastModifiedById { get; set; }
    }
}
