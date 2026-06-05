using DataLayer.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    public class AccountController : Controller
    {
        IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

           

            User user = new User()
            {
                Email = registerViewModel.Email.ToLower(),
                Password = registerViewModel.Password,
                IsAdmin = false,
                RegisteredDate = DateTime.Now
            };

            _userRepository.AddUser(user);

            return View("SuccessRegister", registerViewModel);
        }
        #endregion

        public IActionResult VerifyEmail(string email)
        {
            if (_userRepository.isExistUserByEmail(email.ToLower()))
            {
                return Json($"ایمیل {email} تکراری است");
            }
            return Json(true);
        }

        #region Login


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userRepository.GetUserByEmail(model.Email.ToLower(), model.Password);

            if (user==null)
            {
                ModelState.AddModelError("Email", "اطلاعات صحیح نیست");
                return View(model);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("IsAdmin",user.IsAdmin.ToString())
            };


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent =  model.RememberMe
            };

            await HttpContext.SignInAsync(principal, properties);


            return RedirectToAction("Index","Home");
        }

        #endregion


        #region Logout

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        #endregion
    }
}
