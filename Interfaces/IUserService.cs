using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Interfaces
{
    public interface IUserService
    {
        Task<User> GetForEdit(Guid id);
        Task<bool> Update(Guid id, UserEditViewModel model);
        Task<IEnumerable<User>> GetAll();
        Task<bool> Delete(Guid id);
    }
}
