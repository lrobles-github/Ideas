using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Ideas.Models;


namespace Ideas.Controllers
{
    public class UserController : Controller
    {

        private IdeasContext _context;
 
        public UserController(IdeasContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.errors = new List<string>();
            return View();
        }


        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View("register");
        }


        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserViewModel user)
        {

            System.Console.WriteLine("############# in register controller");
            System.Console.WriteLine("############# " + ModelState.IsValid);
            System.Console.WriteLine("############# " + user.Password + " should equal " + user.PasswordConfirm);

            User AliasChecker = _context.Users.SingleOrDefault(x => x.Alias == user.Alias);

            if (AliasChecker != null)
            {
                ViewBag.AliasError= "User already exists.";
            }

            else if (ModelState.IsValid && AliasChecker == null)
            {
                User newUser = new User {FirstName = user.FirstName, LastName = user.LastName, Alias = user.Alias, Password = user.Password, CreatedAt = DateTime.Now};
                _context.Users.Add(newUser);
                _context.SaveChanges();
                
                HttpContext.Session.SetInt32("LoggedIn", 1);
                HttpContext.Session.SetInt32("UserId", user.Id);
                
                System.Console.WriteLine("############ Redirecting to idea board...");
                return RedirectToAction("ideaboard", "Ideas");
            }

            return View();

        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login(User user)
        {
            System.Console.WriteLine("############ in login controller method");
            
            User UserChecker = _context.Users.SingleOrDefault(x => x.Email == user.Email);

            if (UserChecker != null && user.Password == (string)UserChecker.Password)
            {
                System.Console.WriteLine("############# Compare " + user.Password + " with " + UserChecker.Password);
                HttpContext.Session.SetInt32("LoggedIn", 1);
                HttpContext.Session.SetInt32("UserId", (int)UserChecker.Id);
                return RedirectToAction("ideaboard", "Ideas");
            }
            else
            {
                ViewBag.AliasError= "Wrong Email or password.";
                return View("index");     
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("index");
        }

    }
}
