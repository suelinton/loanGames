using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using com.EmprestimoDeJogos.API.DTOs.Friend;
using com.EmprestimoDeJogos.API.DTOs.Game;
using com.EmprestimoDeJogos.Core.Entities;
using com.EmprestimoDeJogos.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.EmprestimoDeJogos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendController : ControllerBase
    {
        #region Properties
        private readonly IFriendService _friendService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public FriendController(IMapper mapper, IFriendService friendService)
        {
            _mapper = mapper;
            _friendService = friendService;
        }
        #endregion

        #region Public methods
        // GET: api/<FriendController>
        [HttpGet]
        public ActionResult<ListFriendResponse> Get()
        {
            var response = new ListFriendResponse();

            var result = _friendService.GetFriends();

            response.Friends.AddRange(result.Select(_mapper.Map<FriendDto>));

            return response;
        }

        // POST: api/Friend
        [HttpPost]
        public ActionResult<FriendEntity> PostFriend(CreateFriendRequest request)
        {
            var newFriend = new FriendEntity()
            {
                Name = request.Name
            };

            var Friend = _friendService.CreateFriend(newFriend);

            return CreatedAtAction("GetFriend", new { id = Friend.Id }, Friend);
        }

        //GET: api/Friend/5
        [HttpGet("{id}")]
        public ActionResult<FriendDto> GetFriend(int id)
        {
            var Friend = _friendService.GetFriend(id);

            if (Friend == null)
            {
                return NotFound();
            }

            return _mapper.Map<FriendDto>(Friend);
        }

        // PUT: api/Friend/5
        [HttpPut("{id}")]
        public IActionResult PutFriend(int id, FriendDto Friend)
        {
            if (id != Friend.Id)
            {
                return BadRequest();
            }


            try
            {
                _friendService.Update(_mapper.Map<FriendEntity>(Friend));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendExists(id))
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

        // DELETE: api/Friend/5
        [HttpDelete("{id}")]
        public ActionResult<FriendDto> DeleteFriend(int id)
        {
            var Friend = _friendService.GetFriend(id);
            if (Friend == null)
            {
                return NotFound();
            }

            _friendService.Delete(_mapper.Map<FriendEntity>(Friend));
            return _mapper.Map<FriendDto>(Friend);
        }

        [HttpPost("{id}/borrow/{idGame}")]
        public IActionResult Borrow(int id, int idGame)
        {
            if (!FriendExists(id))
            {
                return NotFound("Amigo não existe na base de dados.");
            }

            _friendService.BorrowGame(id, idGame);
            return Ok();
        }
        
        [HttpGet("{id}/borrows")]
        public ActionResult<ListBorrowResponse> Borrow(int id)
        {
            if (!FriendExists(id))
            {
                return NotFound("Amigo não existe na base de dados.");
            }

            var result = _friendService.Borrows(id);

            if(result == null)
            {
                return NoContent();
            }

            var response = new ListBorrowResponse();

            response.Games.AddRange(result.Select(_mapper.Map<GameDto>));

            return response;
        }
        #endregion

        #region Private methods
        private bool FriendExists(int id)
        {
            return _friendService.GetFriends().Any(e => e.Id == id);
        } 
        #endregion

    }
}
