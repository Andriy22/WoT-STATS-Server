using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJWTService
    {
        Task<string> CreateRefreshTokenAsync(AppUser user);
        string CreateToken(AppUser user);
    }
}
