﻿
using Microsoft.AspNetCore.Mvc;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.Services;
using Zadatak1.ViewModels;

namespace Zadatak1.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService, ExceptionHandler exceptionHandler):base(exceptionHandler)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var users = await TryExecute(() => _userService.GetAll());
            return View(users);
        }

        public async Task<IActionResult> Products()
        {
            var users = await TryExecute(() => _userService.GetAll());
            return View(users);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var user = await TryExecute(() => _userService.GetById(id.Value));
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(Guid id, UserEditViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid) return View(model);

            var success = await TryExecute(() => _userService.Update(id, model));
            if (success == null) return NotFound();

            return RedirectToAction("Dashboard", "Admin");
        }

        public async Task<IActionResult> DeleteUser(Guid? id)
        {
            if (id == null) return NotFound();

            var users = await TryExecute(() => _userService.GetAll());
            if (users == null) return NotFound();

            return View(users);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await TryExecute(() => _userService.Delete(id));
            return RedirectToAction("Dashboard", "Admin");
        }

    }
} 
