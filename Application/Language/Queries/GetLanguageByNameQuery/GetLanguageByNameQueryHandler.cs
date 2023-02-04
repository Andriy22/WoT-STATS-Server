using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Language.Queries.GetLanguageByNameQuery
{
    public class GetLanguageByNameQueryHandler : IRequestHandler<GetLanguageByNameQuery, Domain.Language>
    {
        private readonly IDataContext _dataContext;

        public GetLanguageByNameQueryHandler(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Domain.Language> Handle(GetLanguageByNameQuery request, CancellationToken cancellationToken)
        {
            var language = await _dataContext.Languages.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

            if (language == null)
            {
                throw new CustomHttpException($"Language with name [{request.Name}] not found", System.Net.HttpStatusCode.NotFound);
            }

            return language;
        }
    }
}
