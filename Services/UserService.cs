using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetForEdit(Guid id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<bool> Update(Guid id, UserEditViewModel model)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) return false;

            user.UserName = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;
            user.Email = model.Email;
            user.Role = model.Purpose;
            user.LoginPermission = model.Flag;

            _userRepository.Update(user);
            await _userRepository.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) return false;

            _userRepository.Delete(user);
            await _userRepository.SaveChanges();
            return true;
        }
    }
}
