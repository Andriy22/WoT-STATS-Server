using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RefreshToken.Commands.UpdateRefreshTokenCommand
{
    public class UpdateRefreshTokenCommandValidator : AbstractValidator<UpdateRefreshTokenCommand>
    {
        public UpdateRefreshTokenCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
