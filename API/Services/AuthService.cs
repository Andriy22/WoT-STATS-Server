using Application.Common.DTOs.Auth;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.RefreshToken.Commands.UpdateRefreshTokenCommand;
using Application.RefreshToken.Queries.GetRefreshTokenByTokenQuery;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, IMediator mediator, IJWTService jwtService, IConfiguration configuration)
        {
            _userManager = userManager;
            _mediator = mediator;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<AuthorizationVM> AuthorizationAsync(AuthorizationDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new CustomHttpException("Email or password is invalid!");
            }

            var isPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isPassword)
            {
                throw new CustomHttpException("Email or password is invalid!");
            }

            return new AuthorizationVM()
            {
                AccessToken = _jwtService.CreateToken(user),
                RefreshToken = await _jwtService.CreateRefreshTokenAsync(user),
                UserName = model.Email,
                Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                UserId = user.Id,
            };
        }

        public async Task<AuthorizationVM> RefreshAsync(RefreshTokenDTO model)
        {
            var token = await _mediator.Send(new GetRefreshTokenByTokenQuery
            {
                Token = Guid.Parse(model.RefreshToken)
            });

            var refresh_time = _configuration.GetSection("JWT").GetValue<int>("REFRESH_LIFETIME");

            if (token == null)
            {
                throw new CustomHttpException("We can't find your token...", System.Net.HttpStatusCode.BadRequest);
            }

            if (token.IsExpired)
            {
                throw new CustomHttpException("Refresh token is expired...", System.Net.HttpStatusCode.BadRequest);
            }

            if (token.ToLife.AddMinutes(refresh_time) <= DateTime.Now)
            {
                throw new CustomHttpException("Refresh token is expired...", System.Net.HttpStatusCode.BadRequest);
            }

            var handler = new JwtSecurityTokenHandler();
            var decrypt_token = handler.ReadJwtToken(model.AccessToken);

            if (decrypt_token.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value != token.UserId)
            {
                throw new CustomHttpException("Looks like a stolen token!", System.Net.HttpStatusCode.BadRequest);
            }

            await _mediator.Send(new UpdateRefreshTokenCommand
            {
                Id = token.Id,
                IsExpired = true
            });

            var user = await _userManager.FindByIdAsync(token.UserId);

            return new AuthorizationVM()
            {
                AccessToken = _jwtService.CreateToken(user),
                RefreshToken = await _jwtService.CreateRefreshTokenAsync(user),
                UserName = user.Email,
                Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                UserId = user.Id,
            };
        }
    }
}
