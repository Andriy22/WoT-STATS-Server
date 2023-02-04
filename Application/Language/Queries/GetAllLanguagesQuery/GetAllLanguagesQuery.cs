using MediatR;
using Domain;

namespace Application.Language.Queries.GetAllLanguagesQuery
{
    public class GetAllLanguagesQuery : IRequest<List<Domain.Language>> { }
}
