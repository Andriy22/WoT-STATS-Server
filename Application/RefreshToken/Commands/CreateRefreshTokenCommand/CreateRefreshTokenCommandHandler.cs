using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RefreshToken.Commands.CreateRefreshTokenCommand
{
    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, Domain.RefreshToken>
    {
        private readonly IDataContext _dataContext;

        public CreateRefreshTokenCommandHandler(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Domain.RefreshToken> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = new Domain.RefreshToken
            {
                Token = request.Token,
                UserId = request.UserId,
                CreatedAt = request.CreatedAt,
                ToLife = request.ToLife,
                IsExpired = request.IsExpired,
                IpAddress = request.IpAddress
            };

            _dataContext.RefreshTokens.Add(refreshToken);

            await _dataContext.SaveChangesAsync(cancellationToken);

            return refreshToken;
        }
    }
}
