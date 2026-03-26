using Microsoft.EntityFrameworkCore;
using OrderSystem.Application.Interfaces;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Application.Services;

public class ProductService
{
    private readonly IAppDbContext _context;

    public ProductService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(string name, decimal price, int stock)
    {
        var product = new Product(name, price, stock);

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product.Id;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }
}