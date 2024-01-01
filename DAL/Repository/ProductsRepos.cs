using Lab4.Data;
using Lab4.IRepositoryBase;
using Lab4.Models;

namespace Lab4.DAL.Repository;

public class ProductsRepos : IProductsRepository
{
    private readonly DataBaseContext _context;

    public ProductsRepos(DataBaseContext context)
    {
        _context = context;
    }
    public bool Add(Products product)
    {
        _context.Add(product);
        return Save();
    }

    public bool Delete(Products product)
    {
        _context.Remove(product);
        return Save();
    }

    public IEnumerable<Products> GetAll()
    {
        return _context.Products.ToList();
            
    }

    public Products GetById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.ProductID == id);
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
    
    public bool Update(Products product)
    {
        _context.Update(product);
        return Save();
    }
}