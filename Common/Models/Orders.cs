using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4.Models;

public class Orders
{
    [Key] 
    public int OrderID { get; set; }
    public string? Status { get; set; }
    public ICollection<OrderItems>? OrderItems { get; set; }
    public ICollection<Queue>? AmountQueues { get; set; }
    [ForeignKey(nameof(ApplicationUser))]
    public string ApplicationUserID { get; set; }
}