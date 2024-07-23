using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public interface IUserService
    {
        Task<IEnumerable<User>> SelectAllAsync();
        Task<User> DetailsAsync(int? id);
        Task<bool> CreateAsync(User entity);
        User Login(string username, string pwd);
        Task<bool> EditAsync(User entity);
        Task<bool> DeleteAsync(int? id);
    }
}