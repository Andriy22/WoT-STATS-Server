using Application.Common.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Language.Queries.GetUserLanguageQuery
{
    public class GetUserLanguageQueryHandler : IRequestHandler<GetUserLanguageQuery, Domain.Language>
    {
        private readonly IDataContext _dataContext;

        public GetUserLanguageQueryHandler(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Domain.Language> Handle(GetUserLanguageQuery request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null) { 
                throw new CustomHttpException($"User with id [{request.UserId}] not found", System.Net.HttpStatusCode.NotFound);
            }

            return user.Language;
        }
    }
}
