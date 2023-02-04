using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IDataContext
    {
        DbSet<AppUser> Users { get; set; }
        DbSet<Domain.Language> Languages { get; set; }
        DbSet<Domain.RefreshToken> RefreshTokens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
