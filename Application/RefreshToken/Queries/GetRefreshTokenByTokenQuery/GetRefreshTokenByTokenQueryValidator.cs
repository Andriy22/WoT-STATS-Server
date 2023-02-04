using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RefreshToken.Queries.GetRefreshTokenByTokenQuery
{
    public class GetRefreshTokenByTokenQueryValidator : AbstractValidator<GetRefreshTokenByTokenQuery>
    {
        public GetRefreshTokenByTokenQueryValidator()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage("Token is required.");
        }
    }
}
