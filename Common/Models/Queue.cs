using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4.Models;

public class Queue
{
    [Key]
    public int QueueID { get; set; }
    [ForeignKey(nameof(Products))]
    public int ProductID { get; set; }
    [ForeignKey(nameof(Orders))]
    public int OrderID { get; set; }
    public int AmountRequest { get; set; }
    public Products? Product { get; set; }
    public Orders? Order { get; set; }
}