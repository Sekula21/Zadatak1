using Zadatak1.ViewModels;

namespace Zadatak1.Interfaces
{
    public interface IRegistrationService
    {
        Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(RegisterViewModel model);
    }
}
