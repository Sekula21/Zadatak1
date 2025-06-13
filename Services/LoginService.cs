using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Services
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly TokenProvider _tokenProvider;


        public LoginService(SignInManager<User> signInManager, TokenProvider tokenProvider)
        {
            _signInManager = signInManager;
            _tokenProvider = tokenProvider;
        }

        public async Task<(bool Success, string ErrorMessage, string Token)> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
            

            if (result.Succeeded)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(model.Username);
                var token = _tokenProvider.Create(user);
                return (true, null, token);
            }

            return (false, "Username or password is incorrect.", null);
        }
    }
}
