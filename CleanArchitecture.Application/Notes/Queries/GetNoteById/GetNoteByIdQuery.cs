using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Notes.Queries.GetNoteById
{
    public record GetNoteByIdQuery(int Id) : IRequest<GetNoteByIdQueryResponse>;

    public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, GetNoteByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidationService _validationService;

        public GetNoteByIdQueryHandler(IApplicationDbContext context, IValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }

        public async Task<GetNoteByIdQueryResponse> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _validationService.IsValidNote(request.Id))
                {
                    throw new NotFoundException(nameof(Note), request.Id);
                }

                return await _context.Notes
                            .Where(x => x.Id == request.Id && !x.IsDeleted)
                             .AsNoTracking()
                            .Select(x => new GetNoteByIdQueryResponse()
                            {
                                Id = x.Id,
                                Title = x.Title,
                                Content = x.Content,
                                CreatedAtUtc = x.CreatedAtUtc,
                                CreatedById = x.CreatedById,
                                LastModifiedAtUtc = x.LastModifiedAtUtc,
                                LastModifiedById = x.LastModifiedById
                            })
                            .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException(ex.Message);
            }
        }
    }
}
