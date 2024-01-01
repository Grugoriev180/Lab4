using Lab4.Models;

namespace Lab4.IRepositoryBase;

public interface  IUserRepository
{
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    ApplicationUser GetUserById(string id);
    bool Update(ApplicationUser user);
    bool Save();
}