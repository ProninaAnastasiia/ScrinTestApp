namespace ScrinTestApp.Data.Models;

public sealed class Order
{
    public Order()
    {
        Purchases = new HashSet<Purchase>();
    }

    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }
    public decimal Cost { get; set; }

    public User User { get; set; } = null!;
    public ICollection<Purchase> Purchases { get; set; }
}