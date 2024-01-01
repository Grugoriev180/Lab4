using Lab4.Models;

namespace Lab4.IRepositoryBase;

public interface IOrderRepository
{
    Orders GetById(int id);
    bool Add(Orders order);
    bool Delete(Orders order);
    bool Save();
    bool Update(Orders order);
    int GetOrderId();
}