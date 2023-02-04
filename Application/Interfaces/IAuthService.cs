using Application.Common.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthorizationVM> AuthorizationAsync(AuthorizationDTO model);
        Task<AuthorizationVM> RefreshAsync(RefreshTokenDTO model);
    }
}
