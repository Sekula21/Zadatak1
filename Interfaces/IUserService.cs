using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserForEditAsync(Guid id);
        Task<bool> UpdateUserAsync(Guid id, UserEditViewModel model);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(Guid id);
    }
}
