using OrderSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OrderSystem.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Order> Orders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}