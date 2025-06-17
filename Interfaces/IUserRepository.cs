using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetById(Guid id);
        Task<bool> Update(Guid id, UserEditViewModel model);
        Task<IEnumerable<User>> GetAll();
        Task<bool> Delete(Guid id);
    }
}
