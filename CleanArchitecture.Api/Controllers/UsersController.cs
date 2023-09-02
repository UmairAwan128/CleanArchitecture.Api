using CleanArchitecture.Application.Models.Get;
using CleanArchitecture.Application.Users.Commands.DeleteUser;
using CleanArchitecture.Application.Users.Commands.UpdatePassword;
using CleanArchitecture.Application.Users.Commands.UpdateUser;
using CleanArchitecture.Application.Users.Queries.GetUserById;
using CleanArchitecture.Application.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/Users")]
    [Authorize(Roles = "Administrator")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class UsersController : ApiControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets all the Users
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<List<GetUserModel>>> Get([FromQuery] GetUsersQuery query)
        {
            try
            {
                return await Mediator.Send(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failure {typeof(UsersController).Name}, {ex.Message},{DateTime.UtcNow}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a specific user by Id
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserModel>> Get(int id)
        {
            try
            {
                return await Mediator.Send(new GetUserByIdQuery(id));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failure : {typeof(UsersController).Name}, {ex.Message},{DateTime.UtcNow}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Change Password of a User
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("ChangePassword")]
        public async Task<ActionResult<UpdatePasswordCommandResponse>> ChangePassword(UpdatePasswordCommand command)
        {
            try
            {
                await Mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failure : {typeof(UsersController).Name}, {ex.Message},{DateTime.UtcNow}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update details of a User
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserCommandResponse>> Update(int id, UpdateUserCommand command)
        {
            try
            {
                if (id != command.Id)
                {
                    return BadRequest();
                }

                await Mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failure : {typeof(UsersController).Name}, {ex.Message},{DateTime.UtcNow}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a specific User by Id
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteUserCommandResponse>> Delete(int id)
        {
            try
            {
                return await Mediator.Send(new DeleteUserCommand(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failure : {typeof(UsersController).Name}, {ex.Message},{DateTime.UtcNow}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
