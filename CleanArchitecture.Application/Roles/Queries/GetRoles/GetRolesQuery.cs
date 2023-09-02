using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Models.Get;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Roles.Queries.GetRoles
{

    public record GetRolesQuery() : IRequest<List<GetRoleModel>>;

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<GetRoleModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetRolesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetRoleModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Roles
                        .Where(x => !x.IsDeleted)
                        .AsNoTracking()
                         .Select(r => new GetRoleModel()
                         {
                             Id = r.Id,
                             Name = r.Name
                         })
                        .ToListAsync(cancellationToken);
        }
    }
}
