﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<User> _userManager;

        public RegistrationService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool Success, IEnumerable<string> Errors)> Register(RegisterViewModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                Email = model.Email,
                BirthDate = model.BirthDate,
                UserRole = false,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result.Succeeded
                ? (true, null)
                : (false, result.Errors.Select(e => e.Description));
        }
    }
}
