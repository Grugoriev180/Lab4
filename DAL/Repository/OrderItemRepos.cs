using Lab4.Data;
using Lab4.IRepositoryBase;
using Lab4.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4.DAL.Repository;

public class OrderItemRepos : IOrderItemsRepository
{
    private readonly DataBaseContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderItemRepos(DataBaseContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public IEnumerable<OrderItems> GetAllOrderItems()
    {
        return _context.OrderItems.Include(p => p.Product).Include(o => o.Order).ToList();
    }

    public OrderItems GetByOrderID(int id)
    {
        return _context.OrderItems.FirstOrDefault(p => p.OrderID == id);
    }

    public OrderItems GetByProductId(int id)
    {
        return _context.OrderItems.Include(p => p.Product).Include(o => o.Order).FirstOrDefault(p => p.ProductID == id);
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool Add(OrderItems orderItem)
    {
        _context.Add(orderItem);
        return Save();
    }
    public bool Delete(OrderItems orderItem)
    {
        _context.Remove(orderItem);
        return Save();
    }

}