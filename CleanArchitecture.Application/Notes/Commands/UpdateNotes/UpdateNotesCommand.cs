using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Notes.Commands.UpdateNote
{
    public record UpdateNoteCommand : IRequest<UpdateNoteCommandResponse>
    {
        public int Id { get; init; }
        public required string Title { get; init; }
        public string? Content { get; init; }

    }

    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, UpdateNoteCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISecurityContext _securityContext;

        public UpdateNoteCommandHandler(IApplicationDbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        public async Task<UpdateNoteCommandResponse> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Notes), request.Id);
            }

            entity.Title = request.Title;
            entity.Content = request.Content;
            entity.LastModifiedAtUtc = DateTime.UtcNow;
            entity.LastModifiedById = _securityContext.User.Id;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateNoteCommandResponse()
            {
                UpdateNoteRequest = request,
                UpdatedAtUtc = DateTime.UtcNow
            };
        }
    }
}
