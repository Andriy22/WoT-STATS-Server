using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Language.Queries.GetAllLanguagesQuery
{
    public class GetAllLanguagesQueryHandler : IRequestHandler<GetAllLanguagesQuery, List<Domain.Language>>
    {
        private readonly IDataContext _dbContext;

        public GetAllLanguagesQueryHandler(IDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Domain.Language>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Languages.ToListAsync(cancellationToken);
        }
    }
}
