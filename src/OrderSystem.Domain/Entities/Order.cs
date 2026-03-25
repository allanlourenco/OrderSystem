namespace OrderSystem.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public List<OrderItem> Items { get; private set; } = new();
    public decimal Total { get; private set; }

    protected Order()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(Product product, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantidade inválida");

        product.DecreaseStock(quantity);

        var item = new OrderItem(product.Id, quantity, product.Price);

        Items.Add(item);

        Total += product.Price * quantity;
    }
}