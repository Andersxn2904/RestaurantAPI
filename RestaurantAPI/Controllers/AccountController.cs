using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Core.Application.Dtos;
using RestaurantAPI.Core.Application.Interfaces.Services;

namespace RestaurantAPI.Controllers
{
    #region Settings
    [ApiController]
    [Route("api/Account")]
    [ApiVersion("1.0")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion

        #region Register
        [Authorize(Roles = "Admin")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            request.RoleID = 0;
            return Ok(await _accountService.RegisterUserAsync(request, origin));
        }

        [HttpPost("register-waiter")]
        public async Task<IActionResult> RegisterWaiter(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            request.RoleID = 1;
            return Ok(await _accountService.RegisterUserAsync(request, origin));
        }
        #endregion

        #region

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request));
        }
        #endregion
    }
}
