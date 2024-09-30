using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_1_Depi.Data;
using MVC_1_Depi.Models;
using MVC_1_Depi.ViewModels;

namespace MVC_1_Depi.Controllers
{
    public class AccountController : Controller
    {
        private readonly RunDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(RunDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Register Functions
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerviewmodel)
        {
            if(!ModelState.IsValid) return View(registerviewmodel);

            //Model is valid
            var userfound = await _userManager.FindByEmailAsync(registerviewmodel.Email);
            if(userfound != null)
            {
                TempData["Error"] = "Email is already used";
                return View(registerviewmodel);
            }

            //No user Found with this Email
            var newUser = new AppUser()
            {
                UserName = registerviewmodel.Username,
                Email = registerviewmodel.Email,
                PhoneNumber = registerviewmodel.Phone,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerviewmodel.Password);
            if (!newUserResponse.Succeeded)
            {
                TempData["Error"] = "Failed to Register";
                return View(registerviewmodel);
            }

            //if succeeded
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return RedirectToAction("Index", "Club");

        }


        // Login Functions
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginviewmodel)
        {
            if (!ModelState.IsValid) return View(loginviewmodel);

            var userfound = await _userManager.FindByEmailAsync(loginviewmodel.Email);
            if (userfound == null)
            {
                TempData["Error"] = "Email Not Found Please Try Again with a different email";
                return View(loginviewmodel);
            }

            //User is found, check for password
            bool isPasswordValid = await _userManager.CheckPasswordAsync(userfound, loginviewmodel.Password);
            if (!isPasswordValid)
            {
                TempData["Error"] = "Wrong Credentials Please Try Again";
                return View(loginviewmodel);
            }

            //Password is true
            var result = await _signInManager.PasswordSignInAsync(userfound, loginviewmodel.Password, false, false);
            if (!result.Succeeded) return View(loginviewmodel);

            //if login succeeded
            return RedirectToAction("Index", "Club");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Club");

        }
    }
}
