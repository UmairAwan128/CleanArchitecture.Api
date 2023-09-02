using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Notes.Queries.GetNotes
{
    public record GetNotesQuery : IRequest<PaginatedList<GetNotesQueryResponse>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, PaginatedList<GetNotesQueryResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISecurityContext _securityContext;

        public GetNotesQueryHandler(IApplicationDbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        //Get Notes of a the User who is LoggedIn
        public async Task<PaginatedList<GetNotesQueryResponse>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Notes
                        .Where(x => x.CreatedById == _securityContext.User.Id && !x.IsDeleted)
                        .Select(x => new GetNotesQueryResponse()
                        {
                            Id = x.Id,
                            Title = x.Title,
                            CreatedAtUtc = x.CreatedAtUtc,
                            LastModifiedAtUtc = x.LastModifiedAtUtc,
                        })
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
