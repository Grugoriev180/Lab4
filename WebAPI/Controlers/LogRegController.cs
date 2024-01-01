using Lab4.Data;
using Lab4.IRepositoryBase;
using Lab4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controlers;

public class LogRegController : Controller
{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataBaseContext _context;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IQueueRepository _deliveryQueueRepository;

        public LogRegController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, DataBaseContext context,
            IOrderItemsRepository orderItemsRepository, IOrderRepository orderRepository, IQueueRepository queueRepository)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _orderItemsRepository = orderItemsRepository;
            _orderRepository = orderRepository;
            _deliveryQueueRepository = queueRepository;
        }
        public IActionResult Login()
        {
            var response = new LoginModel();
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginModel.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
                TempData["Error"] = "Wrong login details.";
                return BadRequest("Wrong login details.");

            }
            TempData["Error"] = "Wrong email. Please try again";
            return BadRequest("Wrong email. Please try again");
        }
        public IActionResult Register()
        {
            var response = new RegisterModel();
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(registerModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is used";
                return BadRequest("This email address is used");
            }

            var newUser = new ApplicationUser()
            {
                Email = registerModel.EmailAddress,
                UserName = registerModel.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerModel.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, Roles.Client);

            return Ok(); 
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
}