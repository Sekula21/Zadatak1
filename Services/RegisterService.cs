using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<User> _userManager;

        public RegisterService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(RegisterViewModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Name = model.Name,
                LastName = model.LastName,
                Gender = model.Gender,
                Email = model.Gmail,
                BirthDate = model.BirthDate,
                Purpose = false,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result.Succeeded
                ? (true, null)
                : (false, result.Errors.Select(e => e.Description));
        }
    }
}
