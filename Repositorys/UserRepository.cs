using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Repositorys
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ShopContext context) : base(context){}

        public async Task<Product?> GetById(Guid id) => await _context.Products.FindAsync(id);


        public async Task<bool> Update(Guid id, UserEditViewModel model)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Username;
            user.Gender = model.Gender;
            user.UserRole = model.Role;
            user.LoginPermission = model.LoginPermission;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public new async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
