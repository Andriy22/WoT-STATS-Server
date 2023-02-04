using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Language.Queries.GetUserLanguageQuery
{
    public class GetUserLanguageQuery : IRequest<Domain.Language>
    {
        public string UserId { get; set; }
    }
}
