using System.Linq;
using Microsoft.AspNetCore.Mvc;
using com.EmprestimoDeJogos.Core.Interfaces;
using com.EmprestimoDeJogos.API.DTOs.Game;
using com.EmprestimoDeJogos.Core.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace com.EmprestimoDeJogos.API.Controllers
{
    [Route("v1/game")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        #region Properties
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public GamesController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }
        #endregion

        #region Public methods

        // GET: api/Game
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get list of Games",
            Description = "Get list of Games",
            OperationId = "games.list",
            Tags = new[] { "Games" })
        ]
        [SwaggerResponse(200, "ListGameResponse", typeof(ListGameResponse))]
        public ActionResult<ListGameResponse> GetGames()
        {
            var response = new ListGameResponse();

            var result = _gameService.GetGames();

            response.Games.AddRange(result.Select(_mapper.Map<GameDto>));

            return response;
        }

        // POST: api/Game
        [Authorize]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a Game",
            Description = "Create a Game",
            OperationId = "games.create",
            Tags = new[] { "Games" })
        ]
        [SwaggerResponse(401, "UnAuthorized", typeof(UnauthorizedResult))]
        [SwaggerResponse(200, "GameDto", typeof(GameDto))]
        public ActionResult<GameDto> PostGame(CreateGameRequest request)
        {
            var newGame = new GameEntity()
            {
                Name = request.Name
            };

            var game = _gameService.CreateGame(newGame);

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        //GET: api/Game/5
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get a Game by id",
            Description = "Get a Game by id",
            OperationId = "games.get",
            Tags = new[] { "Games" })
        ]
        [SwaggerResponse(200, "GameDto", typeof(GameDto))]
        [SwaggerResponse(404, "Not found Game id", typeof(NotFoundResult))]
        public ActionResult<GameDto> GetGame(int id)
        {
            var game = _gameService.GetGame(id);

            if (game == null)
            {
                return NotFound();
            }

            return _mapper.Map<GameDto>(game);
        }

        // PUT: api/Game/5
        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Update a Game by id",
            Description = "Update a Game by id",
            OperationId = "games.update",
            Tags = new[] { "Games" })
        ]
        [SwaggerResponse(401, "UnAuthorized", typeof(UnauthorizedResult))]
        [SwaggerResponse(200, "GameDto", typeof(GameDto))]
        [SwaggerResponse(400, "Url Id param is not equals to body id object", typeof(BadRequestResult))]
        [SwaggerResponse(404, "Not found Game id", typeof(NotFoundResult))]
        public IActionResult PutGame(int id, GameDto game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }


            try
            {
                _gameService.Update(_mapper.Map<GameEntity>(game));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Game/5
        [Authorize]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a Game by id",
            Description = "Delete a Game by id",
            OperationId = "games.delete",
            Tags = new[] { "Games" })
        ]
        [SwaggerResponse(401, "UnAuthorized", typeof(UnauthorizedResult))]
        [SwaggerResponse(200, "GameDto", typeof(GameDto))]
        [SwaggerResponse(404, "Not found Game id", typeof(NotFoundResult))]
        public ActionResult<GameDto> DeleteGame(int id)
        {
            var game = _gameService.GetGame(id);
            if (game == null)
            {
                return NotFound();
            }

            _gameService.Delete(_mapper.Map<GameEntity>(game));
            return _mapper.Map<GameDto>(game);
        }
        #endregion

        #region Private methods
        private bool GameExists(int id)
        {
            return _gameService.GetGames().Any(e => e.Id == id);
        }
        #endregion
    }
}
