using Cafeteria.Data;
using Cafeteria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace Cafeteria.Controllers
{
    public class AccountsController : Controller
    {
        private readonly CafeteriaContext _context;

        public AccountsController(CafeteriaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]        
        public async Task<IActionResult> LogIn(User _user)
        {
            var query = await _context.User.FirstOrDefaultAsync(m => m.UserEmail == _user.UserEmail && m.Password == _user.Password);
            if(query == null)
            {
                ViewBag.LogInStatus = 0;
            }
            else
            {
                return RedirectToAction("Index", "Accounts");
            }

            return View();
        }
        
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User _user)
        {
            var user = new User
            {
                UserName = _user.UserName,
                UserEmail = _user.UserEmail,
                UserPhone = _user.UserPhone,
                UserAddress = _user.UserAddress,
                Password = _user.Password,
            };

            var query = await _context.User.FirstOrDefaultAsync(t => t.UserEmail == _user.UserEmail);
            if (query != null)
            {
                return BadRequest("Người dùng đã tồn tại!");
            }
            else
            {
                // Thêm người dùng.
                _context.User.Add(user);
                _context.SaveChanges();
                return View();
            }
        }
    }
}
