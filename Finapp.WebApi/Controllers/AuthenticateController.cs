using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Finapp.Authentication;
using Finapp.Authentication.Exception;
using Finapp.Dto.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Finapp.WebApi.Controllers
{
    /// <summary>
    ///     Controller used for authentication
    /// </summary>
    [Route("api/")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <inheritdoc />
        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///     Authenticates user
        /// </summary>
        /// <param name="userLoginInformation">User login information</param>
        /// <returns>Authentication token</returns>
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponseDto>> Authenticate([NotNull]AuthenticationRequestDto userLoginInformation)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }
            AuthenticationResponseDto authentication;
            try
            {
                authentication = await _userService.AuthenticateAsync(userLoginInformation);
            }
            catch (UserNotFoundException ex)
            {
                return Unauthorized(new {message = $"User not found: {ex.UserName}"});
            }
            catch (WrongPasswordException)
            {
                return Unauthorized(new {message = "Wrong password"});
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }

            if (authentication == null)
            {
                return StatusCode(500, "#a001");
            }

            return Ok(authentication);
        }
    }
}