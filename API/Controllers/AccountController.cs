using API.Services;
using Application.Common.DTOs.Registration;
using Application.Interfaces;
using Application.Language.Queries.GetUserLanguageQuery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IRazorRenderService _razorRenderService;
        private readonly IEmailService _emailService;
        private readonly IAccountService _accountService;

        public AccountController(IRazorRenderService razorRenderService, IEmailService emailService, IAccountService accountService)
        {
            _razorRenderService = razorRenderService;
            _emailService = emailService;
            _accountService = accountService;
        }

        [HttpGet("confirm-email/{userId}/{code}")]
        public async Task<ContentResult> ConfirmEmail(string userId, string code)
        {
            await _accountService.ConfirmEmailAsync(userId, code);

            var result = await _razorRenderService.RenderEmailConfirmedViewAsync();

            return new ContentResult
            {
                Content = result,
                ContentType = "text/html"
            };
        }


        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccountAsync(RegistrationDTO model)
        {
            await _accountService.RegistrationAsync(model);

            var url = await _accountService.GenerateEmailConfirmationLinkAsync(model.Email, @"https://localhost:7196/api/account/confirm-email");

            await _emailService.SendEmailAsync(model.Email, "WoT-STATS Email Confirmation", await _razorRenderService.RenderEmailConfirmationAsync(new Application.ViewModels.EmailConfirmationVM()
            {
                ConfirmationLink = url,
                Username = "null"
            }));

            return Ok();
        }
        
        [HttpGet("get-user-language")]
        public async Task<IActionResult> GetUserLanguageAsync()
        {
            return Ok(await Mediator.Send(new GetUserLanguageQuery { UserId = UserId }));
        }
    }
}
