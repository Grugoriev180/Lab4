using Lab4.Models;

namespace Lab4.IRepositoryBase;

public interface  IQueueRepository
{
    List<Queue> GetAllQueues();
    Queue GetByOrderId(int id);
    bool Add(Queue queue);
    bool Delete(Queue queue);
    bool Update(Queue queue);
    bool Save();
}