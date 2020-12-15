using System.Linq;
using AutoMapper;
using com.EmprestimoDeJogos.API.DTOs.Friend;
using com.EmprestimoDeJogos.API.DTOs.Game;
using com.EmprestimoDeJogos.Core.Entities;
using com.EmprestimoDeJogos.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.EmprestimoDeJogos.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    [SwaggerResponse(401, "UnAuthorized", typeof(UnauthorizedResult))]
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
        [SwaggerOperation(
            Summary = "Get list of Friends",
            Description = "Get list of Friends",
            OperationId = "friends.list",
            Tags = new[] { "Friends" })
        ]
        [SwaggerResponse(200, "ListFriendResponse", typeof(ListFriendResponse))]
        public ActionResult<ListFriendResponse> Get()
        {
            var response = new ListFriendResponse();

            var result = _friendService.GetFriends();

            response.Friends.AddRange(result.Select(_mapper.Map<FriendDto>));

            return response;
        }

        // POST: api/Friend
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a Friend",
            Description = "Create a Friend",
            OperationId = "friends.create",
            Tags = new[] { "Friends" })
        ]
        [SwaggerResponse(200, "FriendDto", typeof(FriendDto))]
        public ActionResult<FriendDto> PostFriend(CreateFriendRequest request)
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
        [SwaggerOperation(
            Summary = "Get a Friend by id",
            Description = "Get a Friend by id",
            OperationId = "friends.get",
            Tags = new[] { "Friends" })
        ]
        [SwaggerResponse(200, "FriendDto", typeof(FriendDto))]
        [SwaggerResponse(404, "Not found Friend id", typeof(NotFoundResult))]
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
        [SwaggerOperation(
            Summary = "Update a Friend by id",
            Description = "Update a Friend by id",
            OperationId = "friends.update",
            Tags = new[] { "Friends" })
        ]
        [SwaggerResponse(200, "FriendDto", typeof(FriendDto))]
        [SwaggerResponse(400, "Url Id param is not equals to body id object", typeof(BadRequestResult))]
        [SwaggerResponse(404, "Not found Friend id",typeof(NotFoundResult))]
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
        [SwaggerOperation(
            Summary = "Delete a Friend by id",
            Description = "Delete a Friend by id",
            OperationId = "friends.delete",
            Tags = new[] { "Friends" })
        ]
        [SwaggerResponse(200, "FriendDto", typeof(FriendDto))]
        [SwaggerResponse(404, "Not found Friend id",typeof(NotFoundResult))]
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

        [HttpPost("{id}/borrow/{idFriend}")]
        [SwaggerOperation(
            Summary = "Borrow a game",
            Description = "Borrow a gam",
            OperationId = "friends.borrow",
            Tags = new[] { "Friends" })
        ]
        [SwaggerResponse(200, "FriendDto", typeof(FriendDto))]
        [SwaggerResponse(404, "Not found Friend id",typeof(NotFoundResult))]
        public IActionResult Borrow(int id, int idFriend)
        {
            if (!FriendExists(id))
            {
                return NotFound("Amigo não existe na base de dados.");
            }

            _friendService.BorrowGame(id, idFriend);
            return Ok();
        }
        
        [HttpGet("{id}/borrows")]
        [SwaggerOperation(
            Summary = "Get a list of games loaned to id Friend",
            Description = "Get a list of games loaned to id Friend",
            OperationId = "friends.loans",
            Tags = new[] { "Friends" })
        ]
        [SwaggerResponse(200, "FriendDto", typeof(FriendDto))]
        [SwaggerResponse(404, "Not found Friend id", typeof(NotFoundResult))]
        [SwaggerResponse(204, "There are no borrowed games to Friend id", typeof(NoContentResult))]
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
