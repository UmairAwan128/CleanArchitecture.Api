using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Common
{
    public class ValidationService : IValidationService
    {
        private readonly IApplicationDbContext _context;

        public ValidationService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsValidNote(int Id)
        {
            return await _context.Notes.AnyAsync(x => x.Id == Id && !x.IsDeleted);
        }
    }
}
