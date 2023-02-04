using Application.Common.DTOs.Registration;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Language.Queries.GetLanguageByNameQuery;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public AccountService(UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var decodedCode = System.Text.Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, decodedCode);

            if (!result.Succeeded)
            {
                var error = string.Empty;
                foreach (var item in result.Errors)
                {
                    error += item.Description + Environment.NewLine;
                }

                throw new CustomHttpException(error);
            }
        }

        public async Task<string> GenerateEmailConfirmationLinkAsync(string email, string callbackUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedCode = WebEncoders.Base64UrlEncode(System.Text.Encoding.UTF8.GetBytes(code));
            var confirmationLink = $"{callbackUrl}/{user.Id}/{encodedCode}";
            return confirmationLink;
        }

        public async Task RegistrationAsync(RegistrationDTO model)
        {
            var user = new AppUser()
            {
                Email = model.Email,
                LastLogIn = DateTime.UtcNow,
                CreationTime = DateTime.UtcNow,
                UserName = model.Email,
                Nickname = model.Username,
                LanguageId = (await _mediator.Send(new GetLanguageByNameQuery { Name = model.Language.ToLower() })).Id
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            await _userManager.AddToRoleAsync(user, "User");

            if (!result.Succeeded)
            {
                throw new CustomHttpException(result.Errors.FirstOrDefault().Description);
            }
        }
    }
}
