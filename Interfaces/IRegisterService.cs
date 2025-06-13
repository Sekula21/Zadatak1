using Zadatak1.ViewModels;

namespace Zadatak1.Interfaces
{
    public interface IRegisterService
    {
        Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(RegisterViewModel model);
    }
}
