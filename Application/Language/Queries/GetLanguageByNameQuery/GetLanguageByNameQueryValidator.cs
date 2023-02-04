using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Language.Queries.GetLanguageByNameQuery
{
    public class GetLanguageByNameQueryValidator : AbstractValidator<GetLanguageByNameQuery>
    {
        public GetLanguageByNameQueryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        }
    }
}
