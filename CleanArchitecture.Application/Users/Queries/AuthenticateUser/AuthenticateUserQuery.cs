using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Models.Get;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Queries.AuthenticateUser
{
    public record AuthenticateUserQuery : IRequest<AuthenticateUserQueryResponse>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, AuthenticateUserQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ITokenBuilder _tokenBuilder;


        public AuthenticateUserQueryHandler(IApplicationDbContext context, ITokenBuilder tokenBuilder)
        {
            _context = context;
            _tokenBuilder = tokenBuilder;
        }

        public async Task<AuthenticateUserQueryResponse> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            var user = await (from u in _context.Users
                              where u.Email == request.Email && !u.IsDeleted
                              select u)
                        .Include(x => x.Role)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();


            if (user == null)
            {
                throw new BadRequestException("username/password aren't right");
            }

            if (string.IsNullOrWhiteSpace(request.Password) || !user.Password.VerifyWithBCrypt(request.Password))
            {
                throw new BadRequestException("username/password aren't right");
            }

            var expiresIn = DateTime.Now + TokenAuthOption.ExpiresSpan;
            var token = _tokenBuilder.Build(user.Email, user.Role.Name, expiresIn);

            return new AuthenticateUserQueryResponse
            {
                ExpiresAt = expiresIn,
                Token = token,
                User = new GetUserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = new GetRoleModel()
                    {
                        Id = user.Role.Id,
                        Name = user.Role.Name
                    }
                }
            };
        }
    }
}
