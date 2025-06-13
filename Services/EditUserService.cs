using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Services
{
    public class UserService : IEditUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserForEditAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateUserAsync(Guid id, UserEditViewModel model)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            user.UserName = model.Username;
            user.Name = model.Name;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.Email = model.Gmail;
            user.Purpose = model.Purpose;
            user.Flag = model.Flag;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}
