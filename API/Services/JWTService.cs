using Application.Interfaces;
using Application.RefreshToken.Commands.CreateRefreshTokenCommand;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JWTService(IConfiguration configuration, UserManager<AppUser> userManager, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> CreateRefreshTokenAsync(AppUser user)
        {
            var token = await _mediator.Send(new CreateRefreshTokenCommand
            {
                CreatedAt = DateTime.UtcNow,
                ToLife = DateTime.Now.AddMinutes(_configuration.GetSection("JWT").GetValue<int>("REFRESH_LIFETIME")),
                IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                IsExpired = false,
                Token = Guid.NewGuid(),
                UserId = user.Id,
            });

            return token.Token.ToString();
        }

        public string CreateToken(AppUser user)
        {
            var identity = GetIdentity(user);

            var now = DateTime.UtcNow;

            var KEY = _configuration.GetSection("JWT").GetValue<string>("KEY");

            var SSK = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));

            var jwt = new JwtSecurityToken(
                    issuer: _configuration.GetSection("JWT").GetValue<string>("ISSUER"),
                    audience: _configuration.GetSection("JWT").GetValue<string>("AUDIENCE"),
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.AddMinutes(_configuration.GetSection("JWT").GetValue<int>("LIFETIME")),
                    signingCredentials: new SigningCredentials(SSK, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(AppUser user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id),
                };
            var roles = _userManager.GetRolesAsync(user).Result.ToList();
            foreach (var el in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, el));
            }
            claims.Add(new Claim("email", user.Email));
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
