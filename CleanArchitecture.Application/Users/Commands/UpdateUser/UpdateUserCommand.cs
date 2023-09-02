using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest<UpdateUserCommandResponse>
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int RoleId { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISecurityContext _securityContext;

        public UpdateUserCommandHandler(IApplicationDbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var roleExists = await _context.Roles.AnyAsync(x => x.Id == request.RoleId);

            if (!roleExists)
            {
                throw new NotFoundException($"Role with Id - {request.RoleId} is not found");
            }

            if (!request.Email.IsValidEmailAddress())
            {
                throw new BadRequestException($"Email Address - {request.Email} is not in Valid Format.");
            }


            var entity = await _context.Users
                                .Where(x => x.Id == request.Id && !x.IsDeleted)
                                .FirstOrDefaultAsync(cancellationToken);

            entity.Email = request.Email;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.RoleId = request.RoleId;
            entity.LastModifiedById = _securityContext.User.Id;
            entity.LastModifiedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateUserCommandResponse()
            {
                UpdatedAtUtc = DateTime.UtcNow,
                UpdateUserRequest = request
            };
        }
    }
}
