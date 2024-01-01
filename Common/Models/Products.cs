using System.ComponentModel.DataAnnotations;

namespace Lab4.Models;

public class Products
{
    [Key]
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public int Price { get; set; }
    public int Amount { get; set; }
    public ICollection<OrderItems> OrderItems { get; set; }
    public ICollection<Queue> Queues { get; set; }
}