using Lab4.Data;
using Lab4.IRepositoryBase;
using Lab4.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4.DAL.Repository;

public class UserRepos : IUserRepository
{
    private readonly DataBaseContext _context;

    public UserRepos(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public ApplicationUser GetUserById(string id)
    {
        return _context.Users.Find(id);
    }

    public bool Update(ApplicationUser user)
    {
        _context.Users.Update(user);
        return Save();
    }
    
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}