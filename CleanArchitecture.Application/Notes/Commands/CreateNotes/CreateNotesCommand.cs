using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Notes.Commands.CreateNote
{
    public record CreateNoteCommand : IRequest<CreateNoteCommandResponse>
    {
        public required string Title { get; set; }
        public string? Content { get; set; }
    }

    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, CreateNoteCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISecurityContext _securityContext;

        public CreateNoteCommandHandler(IApplicationDbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        public async Task<CreateNoteCommandResponse> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = new Note
            {
                Title = request.Title,
                Content = request.Content,
                CreatedById = _securityContext.User.Id,
                CreatedAtUtc = DateTime.UtcNow
            };

            _context.Notes.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateNoteCommandResponse()
            {
                Id = entity.Id,
                CreateAtUtc = entity.CreatedAtUtc,
                NoteRequest = request
            };
        }
    }
}
