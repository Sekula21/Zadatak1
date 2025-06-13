using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zadatak1.Models;
using Zadatak1.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Zadatak1.Interfaces;

namespace Zadatak1.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;

        public AccountsController(ILoginService loginService, IRegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (success, errorMessage, token) = await _loginService.LoginAsync(model);
                if (success && model.Flag == true)
                {
                    TempData[token] = token;
                    if (model.Purpose == true)
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else if (model.Purpose == false)
                    {
                        return RedirectToAction("Products", "User");
                    }
                }else if(model.Flag == false)
                {
                    ModelState.AddModelError("You have been restricted to login!", errorMessage);
                }

                ModelState.AddModelError("", errorMessage);
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (success, errors) = await _registerService.RegisterAsync(model);

                if (success)
                {
                    return RedirectToAction("Login", "Accounts");
                }

                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(model);

        }
    }
}
