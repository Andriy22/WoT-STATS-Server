using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Language.Queries.GetUserLanguageQuery
{
    public class GetUserLanguageQueryValidator : AbstractValidator<GetUserLanguageQuery>
    {
        public GetUserLanguageQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required.");
        }
    }
}
