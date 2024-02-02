namespace ScrinTestApp.Data.Models;

public sealed class User
{
    public User()
    {
        Orders = new HashSet<Order>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;

    public ICollection<Order> Orders { get; set; }
}