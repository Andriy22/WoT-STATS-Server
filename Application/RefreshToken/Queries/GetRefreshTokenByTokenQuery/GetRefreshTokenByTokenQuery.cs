using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RefreshToken.Queries.GetRefreshTokenByTokenQuery
{
    public class GetRefreshTokenByTokenQuery : IRequest<Domain.RefreshToken>
    {
        public Guid Token { get; set; }
    }
}
