using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RefreshToken.Queries.GetRefreshTokensByUserIdQuery
{
    public class GetRefreshTokensByUserIdQuery : IRequest<List<Domain.RefreshToken>>
    {
        public string UserId { get; set; }
    }
}
