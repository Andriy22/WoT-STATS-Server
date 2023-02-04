using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Language.Queries.GetLanguageByNameQuery
{
    public class GetLanguageByNameQuery : IRequest<Domain.Language>
    {
        public string Name { get; set; }
    }

}
