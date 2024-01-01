using Lab4.IRepositoryBase;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controlers;

public class UserController: Controller
{
    private readonly IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("users")]
    public async Task<IActionResult> Index()
    {
        var users = await _userRepository.GetAllUsersAsync();
        List<ClientModel> result = new List<ClientModel>();
        foreach (var user in users)
        {
            var userViewModel = new ClientModel
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email
            };
            result.Add(userViewModel);
        }
        return Json(result);
    }

    [HttpGet("users/{id}")]
    public async Task<IActionResult> Detail(string id)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        var userDetailViewModel = new ClientModel()
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email
        };
        return Json(userDetailViewModel);
    }
}