using Application.Common.DTOs.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task RegistrationAsync(RegistrationDTO model);
        Task<string> GenerateEmailConfirmationLinkAsync(string email, string callbackUrl);
        Task ConfirmEmailAsync(string userId, string code);
    }
}
