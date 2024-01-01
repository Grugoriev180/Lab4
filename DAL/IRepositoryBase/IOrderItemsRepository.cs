using Lab4.Models;

namespace Lab4.IRepositoryBase;

public interface IOrderItemsRepository
{
    IEnumerable<OrderItems> GetAllOrderItems();
    OrderItems GetByOrderID(int id);
    OrderItems GetByProductId(int id);
    bool Add(OrderItems orderItems);
    bool Delete(OrderItems orderItem);
    bool Save();
}