using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Models.Get;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Queries.GetUsers
{
    public record GetUsersQuery() : IRequest<List<GetUserModel>>;

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<GetUserModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetUsersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetUserModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                        .Where(x => !x.IsDeleted)
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
                        .ToListAsync(cancellationToken);
        }
    }
}
