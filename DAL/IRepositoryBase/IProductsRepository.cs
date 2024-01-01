using Lab4.Models;

namespace Lab4.IRepositoryBase;

public interface IProductsRepository
{
    IEnumerable<Products> GetAll();
    Products GetById(int id);
    bool Add(Products product);
    bool Delete(Products product);
    bool Save();
    bool Update(Products product);
}