using Lab4.Data;
using Lab4.IRepositoryBase;
using Lab4.Models;

namespace Lab4.DAL.Repository;

public class OrderRepos : IOrderRepository
{
    private readonly DataBaseContext _context;

    public OrderRepos(DataBaseContext context)
    {
        _context = context;
    }

    public Orders GetById(int id)
    {
        return _context.Orders.Find(id);
    }
    
    public bool Add(Orders order)
    {
        _context.Add(order);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
    public bool Delete(Orders order)
    {
        _context.Remove(order);
        return Save();
    }
    public bool Update(Orders order)
    {
        _context.Update(order);
        return Save();
    }
    
    public int GetOrderId()
    {
        return _context.Orders.Max(order => order.OrderID);
    }
}