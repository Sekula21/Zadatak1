﻿using Zadatak1.Models;
using Zadatak1.ViewModels;

namespace Zadatak1.Interfaces
{
    public interface IUserService
    {
        Task<User> GetById(Guid id);
        Task<string> Update(Guid id, UserEditViewModel model);
        Task<IEnumerable<User>> GetAll();
        Task<string> Delete(Guid id);
    }
}
