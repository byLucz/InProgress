using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XKANBAN.DTOs.Users;
using XKANBAN.Models.User;
using XKANBAN.Services.Interfaces;
using XKANBAN.Contxet;

namespace XKANBAN.Controllers
{
    public class UserController : Controller
    {
        IUserService _userService;
        KanBanContext _context;

        public UserController(KanBanContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_userService.IsUsernameExist(model.Username))
            {
                ModelState.AddModelError("Логин", "Такой логин уже существует, пожалуйста, введите другой");
                return View(model);
            }

            if (_userService.IsEmailExist(model.Email))
            {
                ModelState.AddModelError("Почта", "Такой почтовый ящик уже существует, пожалуйста, введите другой");
                return View(model);
            }

            User user = new User()
            {
                FullName = model.FullName,
                Username = model.Username.ToLower(),
                Email = model.Email.ToLower(),
                IsDelete = false,
                Password = model.Password,
                RegisterDate = DateTime.Now,
                Role = model.Role,
                Group = model.Group
            };

            _userService.AddUser(user);

            return Redirect("/Project/SuperPanel");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model, string ReturnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.LoginUser(model);

            if (user != null)
            {
                var claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Email)
                };

                var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = model.RememberMe
                };

                HttpContext.SignInAsync(principal, properties);

                if (ReturnUrl != "")
                {
                    return Redirect(ReturnUrl);
                }

                return Redirect("/Project/Desks");
            }
            else
            {
                ModelState.AddModelError("Username", "user not found!");
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult UserProfile()
        {
            var userEmail = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserProfileViewModel
            {
                Email = user.Email,
                Login = user.Username,
                FullName = user.FullName,
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult UserProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userEmail = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound();
            }

            user.Username = model.Login;
            user.Email = model.Email;
            user.FullName = model.FullName;

            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = model.Password;
            }

            _context.SaveChanges();

            ViewBag.Message = "Ваш профиль успешно обновлен";
            return View(model);
        }

        public IActionResult IsSupervisor()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            var isSupervisor = user != null && user.Role == "Supervisor";
            return Json(new { isSupervisor });
        }

        public IActionResult IsTeacher()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            var isTeacher = user != null && user.Role == "Teacher";
            return Json(new { isTeacher });
        }

        public IActionResult IsStudent()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            var isStudent = user != null && user.Role == "Student";
            return Json(new { isStudent });
        }

        public IActionResult IsConsultant()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            var isConsultant = user != null && user.Role == "Consultant";
            return Json(new { isConsultant });
        }
    }
}