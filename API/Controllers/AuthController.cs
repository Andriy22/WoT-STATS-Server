using Application.Common.DTOs.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("authorization")]
        public async Task<IActionResult> AuthorizationAsync(AuthorizationDTO model)
        {
            return Ok(await _authService.AuthorizationAsync(model));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync(RefreshTokenDTO model)
        {
            return Ok(await _authService.RefreshAsync(model));
        }
    }
}
