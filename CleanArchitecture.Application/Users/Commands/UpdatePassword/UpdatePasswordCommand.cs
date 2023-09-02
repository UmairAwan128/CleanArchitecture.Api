using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Commands.UpdatePassword
{
    public record UpdatePasswordCommand : IRequest<UpdatePasswordCommandResponse>
    {
        public int UserId { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }

    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISecurityContext _securityContext;

        public UpdatePasswordCommandHandler(IApplicationDbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users
                                .Where(x => x.Id == request.UserId && !x.IsDeleted)
                                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            if (!entity.Password.VerifyWithBCrypt(request.CurrentPassword))
            {
                throw new BadRequestException("Current Password isn't right");
            }

            entity.Password = request.NewPassword.Trim().WithBCrypt();
            entity.LastModifiedById = _securityContext.User.Id;
            entity.LastModifiedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdatePasswordCommandResponse()
            {
                UpdatedAtUtc = DateTime.UtcNow,
                UpdatePasswordRequest = request
            };
        }
    }
}
