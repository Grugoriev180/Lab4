using Lab4.Data;
using Lab4.IRepositoryBase;
using Lab4.Models;

namespace Lab4.DAL.Repository;

public class QueueRepos : IQueueRepository
{
    private readonly DataBaseContext _context;

    public QueueRepos(DataBaseContext context)
    {
        _context = context;
    }
    
    public Queue GetByOrderId(int id)
    {
        return _context.Queues.FirstOrDefault(p => p.OrderID == id);
    }
    
    public List<Queue> GetAllQueues()
    {
        return _context.Queues.ToList();
    }
    
    public bool Add(Queue deliveryQueue)
    {
        _context.Add(deliveryQueue);
        return Save();
    }
    public bool Delete(Queue deliveryQueue)
    {
        _context.Remove(deliveryQueue);
        return Save();
    }
    public bool Update(Queue deliveryQueue)
    {
        _context.Update(deliveryQueue);
        return Save();
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}