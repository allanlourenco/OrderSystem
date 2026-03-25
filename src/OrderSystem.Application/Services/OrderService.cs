using Microsoft.EntityFrameworkCore;
using OrderSystem.Application.Interfaces;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Application.Services;

public class OrderService
{
    private readonly IAppDbContext _context;

    public OrderService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateOrderAsync(Guid productId, int quantity)
    {
        // 🔁 retry simples para concorrência
        for (int attempt = 0; attempt < 3; attempt++)
        {
            try
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == productId);

                if (product == null)
                    throw new Exception("Produto não encontrado");

                var order = new Order();

                order.AddItem(product, quantity);

                _context.Orders.Add(order);

                await _context.SaveChangesAsync();

                return order.Id;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (attempt == 2)
                    throw;
            }
        }

        throw new Exception("Erro ao processar pedido");
    }
}