using Lab4.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data;

public class DataBaseContext : IdentityDbContext<ApplicationUser>
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options){}
    public DbSet<Queue> Queues{ get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Products> Products { get; set; }
}