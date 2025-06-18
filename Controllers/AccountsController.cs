using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zadatak1.Models;
using Zadatak1.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Zadatak1.Interfaces;
using Zadatak1.Services;

namespace Zadatak1.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly IResponseMessageService _responseMessageService;
        private readonly ILoginService _loginService;
        private readonly IRegistrationService _registrationService;

        public AccountsController(ILoginService loginService, IRegistrationService registerService,
            IResponseMessageService responseMessageService, ExceptionHandler exceptionHandler):base(exceptionHandler)
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

                switch (success)
                {
                    case true when model.LoginPermission:
                        TempData[token] = token;

                        return model.Role switch
                        {
                            true => RedirectToAction("Dashboard", "Admin"),
                            false => RedirectToAction("Products", "User"),
                        };

                    case true when !model.LoginPermission:
                        var loginError = _responseMessageService.Get("Errors", "RestricedLogin");
                        return BadRequest(loginError);

                    case false:
                        ModelState.AddModelError("", errorMessage);
                        break;
                }
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
                var (success, errors) = await _registrationService.Register(model);

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

