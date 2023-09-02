using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<CreateUserCommandResponse>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int RoleId { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISecurityContext _securityContext;

        public CreateUserCommandHandler(IApplicationDbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!_securityContext.IsAdministrator)
            {
                throw new BadRequestException($"Only Administrators are Allowed To Create New Users.");
            }

            var roleExists = await _context.Roles.AnyAsync(x => x.Id == request.RoleId);

            if (!roleExists)
            {
                throw new NotFoundException($"Role with Id - {request.RoleId} is not found");
            }

            if (!request.Email.IsValidEmailAddress())
            {
                throw new BadRequestException($"Email Address - {request.Email} is not in Valid Format.");
            }

            var entity = new User
            {
                Email = request.Email,
                Password = request.Password.WithBCrypt(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAtUtc = DateTime.UtcNow,
                RoleId = request.RoleId,
                CreatedById = _securityContext.User.Id
            };

            _context.Users.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateUserCommandResponse()
            {
                Id = entity.Id,
                CreatedAtUtc = entity.CreatedAtUtc,
                CreateUserRequest = request
            };
        }
    }
}
