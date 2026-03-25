namespace OrderSystem.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    // Controle de concorrência (IMPORTANTE)
    public byte[] RowVersion { get; private set; }

    protected Product() { }

    public Product(string name, decimal price, int stock)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantidade inválida");

        if (Stock < quantity)
            throw new InvalidOperationException("Estoque insuficiente");

        Stock -= quantity;
    }
}