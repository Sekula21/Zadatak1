
using Microsoft.AspNetCore.Mvc;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.Services;
using Zadatak1.ViewModels;

namespace Zadatak1.Controllers
{
    public class EditUsersController : Controller
    {
        private readonly IEditUserService _userService;

        public EditUsersController(IEditUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var user = await _userService.GetUserForEditAsync(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(Guid id, UserEditViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid) return View(model);

            var success = await _userService.UpdateUserAsync(id, model);
            if (!success) return NotFound();

            return RedirectToAction("Dashboard", "Admin");
        }

        public async Task<IActionResult> DeleteUser(Guid? id)
        {
            if (id == null) return NotFound();

            var users = await _userService.GetAllUsersAsync();
            if (users == null) return NotFound();

            return View(users);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction("Dashboard", "Admin");
        }

    }
} 
