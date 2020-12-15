using System.Collections.Generic;
using System.Threading.Tasks;
using com.EmprestimoDeJogos.API.DTOs;
using com.EmprestimoDeJogos.Core.Services;
using com.EmprestimoDeJogos.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace com.EmprestimoDeJogos.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenClaimsService _tokenClaimsService;


        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenClaimsService tokenClaimsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenClaimsService = tokenClaimsService;
        }

        [HttpPost("Criar")]
        [SwaggerOperation(
            Summary = "Creates a new user",
            Description = "Creates a user",
            OperationId = "authenticate.create",
            Tags = new[] { "Authenticate" })
        ]
        [SwaggerResponse(200, "UserToken", typeof(UserToken))]
        [SwaggerResponse(400, "List of error", typeof(IEnumerable<IdentityError>))]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            string token;

            if (result.Succeeded)
            {
               token = await _tokenClaimsService.GetTokenAsync(model.Email);

                return new UserToken()
                {
                    Token = token
                };
            }
            else
            {
                return BadRequest(result.Errors);
            }

            
        }


        [HttpPost("Login")]
        [SwaggerOperation(
            Summary = "Authenticates a user",
            Description = "Authenticates a user",
            OperationId = "authenticate.login",
            Tags = new[] { "Authenticate" })
        ]
        [SwaggerResponse(200, "UserToken", typeof(UserToken))]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password,
                 isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new UserToken()
                {
                    Token = await _tokenClaimsService.GetTokenAsync(userInfo.Email)
                };
            }
            else
            {
                ModelState.AddModelError(string.Empty, "login inválido.");
                return BadRequest(ModelState);
            }
        }
    }
}
