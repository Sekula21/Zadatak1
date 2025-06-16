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
        private readonly IResponseMessageService _responseMessageService;
        private readonly ILoginService _loginService;
        private readonly IRegistrationService _registrationService;

        public AccountsController(ILoginService loginService, IRegistrationService registerService, IResponseMessageService responseMessageService)
        {
            _loginService = loginService;
            _registrationService = registerService;
            _responseMessageService = responseMessageService;
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
                if (success && model.LoginPermission == true)
                {
                    TempData[token] = token;
                    if (model.Role == true)
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else if (model.Role == false)
                    {
                        return RedirectToAction("Products", "User");
                    }
                }else if(model.LoginPermission == false)
                {
                    var loginError = _responseMessageService.Get("Errors", "RestricedLogin");
                    return BadRequest(loginError);
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
                var (success, errors) = await _registrationService.RegisterAsync(model);

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
