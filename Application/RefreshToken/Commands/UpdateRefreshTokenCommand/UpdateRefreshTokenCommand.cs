using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RefreshToken.Commands.UpdateRefreshTokenCommand
{
    public class UpdateRefreshTokenCommand : IRequest
    {
        public int Id { get; set; }
        public bool IsExpired { get; set; }
    }
}
