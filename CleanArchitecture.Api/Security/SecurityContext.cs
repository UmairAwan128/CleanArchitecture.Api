namespace CleanArchitecture.Api.Security
{
    //public class SecurityContext : ISecurityContext
    //{
    //    private readonly IHttpContextAccessor _contextAccessor;
    //    private readonly IMediator _mediator;
    //    private GetUserModel _user;

    //    public SecurityContext(IHttpContextAccessor contextAccessor, IApplicationDbContext context, IMediator mediator)
    //    {
    //        _contextAccessor = contextAccessor;
    //        _mediator = mediator;
    //    }

    //    public GetUserModel User
    //    {
    //        get
    //        {
    //            if (_user != null) return _user;

    //            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
    //            {
    //                throw new UnauthorizedAccessException();
    //            }

    //            _user = Task.Run(GetUserDetails).Result;

    //            if (_user == null)
    //            {
    //                throw new UnauthorizedAccessException("User is not found");
    //            }

    //            return _user;
    //        }
    //    }

    //    public bool IsAdministrator
    //    {
    //        get { return User.Role.Name == Roles.Administrator; }
    //    }

    //    private async Task<GetUserModel> GetUserDetails()
    //    {
    //        var email = _contextAccessor.HttpContext.User.Identity.Name;
    //        return await _mediator.Send(new GetUserByEmailQuery() { Email = email });
    //    }
    //}
}