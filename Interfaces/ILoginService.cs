using Zadatak1.ViewModels;

namespace Zadatak1.Interfaces
{
    public interface ILoginService
    {
        Task<(bool Success, string ErrorMessage, string Token)> LoginAsync(LoginViewModel model);
    }
}
