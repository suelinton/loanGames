using System.Linq;
using Microsoft.AspNetCore.Mvc;
using com.EmprestimoDeJogos.Core.Interfaces;
using com.EmprestimoDeJogos.API.DTOs.Game;
using com.EmprestimoDeJogos.Core.Entities;
using AutoMapper;

namespace com.EmprestimoDeJogos.API.Controllers
{
    [Route("api/[controller]")]
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

        // GET: api/Games
        [HttpGet]
        public ActionResult<ListGameResponse> GetGames()
        {
            var response = new ListGameResponse();

            var result = _gameService.GetGames();

            response.Games.AddRange(result.Select(_mapper.Map<GameDto>));

            return response;
        }

        // POST: api/Games
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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

        //GET: api/Games/5
        [HttpGet("{id}")]
        public ActionResult<GameDto> GetGame(int id)
        {
            var game = _gameService.GetGame(id);

            if (game == null)
            {
                return NotFound();
            }

            return new GameDto()
            {
                Id = game.Id,
                Name = game.Name
            };
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGame(int id, Game game)
        //{
        //    if (id != game.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _gameService.Entry(game).State = EntityState.Modified;

        //    try
        //    {
        //        await _gameService.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GameExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // DELETE: api/Games/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Game>> DeleteGame(int id)
        //{
        //    var game = await _gameService.Games.FindAsync(id);
        //    if (game == null)
        //    {
        //        return NotFound();
        //    }

        //    _gameService.Games.Remove(game);
        //    await _gameService.SaveChangesAsync();

        //    return game;
        //}

        //private bool GameExists(int id)
        //{
        //    return _gameService.Games.Any(e => e.Id == id);
        //}
    }
}
