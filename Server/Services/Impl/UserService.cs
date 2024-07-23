using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public class UserService : IUserService
    {
        private readonly vkspDbContext _context;

        public UserService(vkspDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<User>> SelectAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var entity = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<bool> CreateAsync(User entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public User Login(string username, string pwd) 
        {
            var user =  _context.Users.FirstOrDefault(x=>x.Username==username && x.Password == pwd);

            return user;
        }

        public async Task<bool> EditAsync(User entity)
        {
            try
            {
                _context.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(entity.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return false;
            }

            var entity = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return false;
            }

            _context.Users.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        private bool UserExists(int? id)
        {
            return _context.Users.Any(m => m.Id == id);
        }
    }
}