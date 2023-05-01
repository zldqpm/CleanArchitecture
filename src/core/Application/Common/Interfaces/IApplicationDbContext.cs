using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Device> Devices { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
