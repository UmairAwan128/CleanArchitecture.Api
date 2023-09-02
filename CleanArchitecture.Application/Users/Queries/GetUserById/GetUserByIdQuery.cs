using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Models.Get;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(int Id) : IRequest<GetUserModel>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserModel>
    {
        private readonly IApplicationDbContext _context;

        public GetUserByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(x => x.Id == request.Id && !x.IsDeleted)
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
