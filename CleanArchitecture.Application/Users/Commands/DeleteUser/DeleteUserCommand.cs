using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(int Id) : IRequest<DeleteUserCommandResponse>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISecurityContext _securityContext;

        public DeleteUserCommandHandler(IApplicationDbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users
                                .Where(x => x.Id == request.Id && !x.IsDeleted)
                                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            entity.IsDeleted = true;
            entity.LastModifiedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteUserCommandResponse()
            {
                UpdatedAtUtc = DateTime.UtcNow,
                DeleteUserRequest = request
            };
        }
    }
}
