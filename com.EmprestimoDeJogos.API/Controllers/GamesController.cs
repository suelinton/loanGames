using System.Linq;
using Microsoft.AspNetCore.Mvc;
using com.EmprestimoDeJogos.Core.Interfaces;
using com.EmprestimoDeJogos.API.DTOs.Game;
using com.EmprestimoDeJogos.Core.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace com.EmprestimoDeJogos.API.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GamesController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        // GET: api/Game
        [HttpGet]
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
        public ActionResult<GameEntity> PostGame(CreateGameRequest request)
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

        private bool GameExists(int id)
        {
            return _gameService.GetGames().Any(e => e.Id == id);
        }
    }
}
