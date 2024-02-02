namespace ScrinTestApp.Data.Models;

public sealed class Product
{
    public Product()
    {
        Purchases = new HashSet<Purchase>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Amount { get; set; }

    public ICollection<Purchase> Purchases { get; set; }
}