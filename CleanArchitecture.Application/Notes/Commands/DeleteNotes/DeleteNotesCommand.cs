using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Notes.Commands.DeleteNote
{
    public record DeleteNoteCommand(int Id) : IRequest<DeleteNoteCommandResponse>;

    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, DeleteNoteCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISecurityContext _securityContext;

        public DeleteNoteCommandHandler(IApplicationDbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        public async Task<DeleteNoteCommandResponse> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Notes), request.Id);
            }

            entity.IsDeleted = true;
            entity.LastModifiedAtUtc = DateTime.UtcNow;
            entity.LastModifiedById = _securityContext.User.Id;

            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteNoteCommandResponse()
            {
                UpdatedAtUtc = DateTime.UtcNow,
                DeleteNoteRequest = request
            };
        }
    }

}
