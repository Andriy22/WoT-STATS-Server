using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RefreshToken.Queries.GetRefreshTokensByUserIdQuery
{
    public class GetRefreshTokensByUserIdQueryValidator : AbstractValidator<GetRefreshTokensByUserIdQuery>
    {
        public GetRefreshTokensByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required.");
        }
    }
}
