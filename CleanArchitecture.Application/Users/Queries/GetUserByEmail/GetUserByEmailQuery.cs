using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Models.Get;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Queries.GetUserByEmail
{
    public record GetUserByEmailQuery : IRequest<GetUserModel>
    {
        public required string Email { get; set; }
    }

    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, GetUserModel>
    {
        private readonly IApplicationDbContext _context;

        public GetUserByEmailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserModel> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(x => x.Email == request.Email && !x.IsDeleted)
                .Include(x => x.Role)
                .AsNoTracking()
                .Select(u => new GetUserModel()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Role = new GetRoleModel()
                    {
                        Id = u.Role.Id,
                        Name = u.Role.Name
                    }
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
