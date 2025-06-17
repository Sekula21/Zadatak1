using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.Repositorys;
using Zadatak1.ViewModels;

namespace Zadatak1.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetById(Guid id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<string> Update(Guid id, UserEditViewModel model)
        {
            var success = await _userRepository.Update(id, model);
            if (!success)
                return "User not found or update failed.";

            return "User successfully updated.";
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<string> Delete(Guid id)
        {
            var success = await _userRepository.Delete(id);
            if (!success)
                return "User not found or delete failed.";

            return "User deleted successfully.";
        }
    }
}
