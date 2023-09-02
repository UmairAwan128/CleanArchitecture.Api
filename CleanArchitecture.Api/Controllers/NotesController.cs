using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Notes.Commands.CreateNote;
using CleanArchitecture.Application.Notes.Commands.DeleteNote;
using CleanArchitecture.Application.Notes.Commands.UpdateNote;
using CleanArchitecture.Application.Notes.Queries.GetNoteById;
using CleanArchitecture.Application.Notes.Queries.GetNotes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/Notes")]
    [Authorize(Roles = "Administrator, User")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class NotesController : ApiControllerBase
    {
        private readonly ILogger<NotesController> _logger;

        public NotesController(ILogger<NotesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets all the Notes that belong to the User who is currently LoggedIn.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<GetNotesQueryResponse>>> Get([FromQuery] GetNotesQuery query)
        {
            try
            {
                return await Mediator.Send(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failure : {typeof(NotesController).Name}, {ex.Message},{DateTime.UtcNow}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a specific Note by Id
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetNoteByIdQueryResponse>> Get(int id)
        {
            try
            {
                return await Mediator.Send(new GetNoteByIdQuery(id));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failure : {typeof(NotesController).Name}, {ex.Message},{DateTime.UtcNow}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates a new Note
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<CreateNoteCommandResponse>> Create(CreateNoteCommand command)
        {
            try
            {
                return await Mediator.Send(command);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failure : {typeof(NotesController).Name}, {ex.Message},{DateTime.UtcNow}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update an existing Note details
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateNoteCommandResponse>> Update(int id, UpdateNoteCommand command)
        {
            try
            {
                if (id != command.Id)
                {
                    return BadRequest();
                }

                return await Mediator.Send(command);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failure : {typeof(NotesController).Name}, {ex.Message},{DateTime.UtcNow}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a Note by Id
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteNoteCommandResponse>> Delete(int id)
        {
            try
            {
                return await Mediator.Send(new DeleteNoteCommand(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failure : {typeof(NotesController).Name}, {ex.Message},{DateTime.UtcNow}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
