using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public class TodoService : ITodoService
    {
        private readonly vkspDbContext _context;
        
        public TodoService(vkspDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Todo>> SelectAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }
        
        public async Task<IEnumerable<TodoNeed>> GetAllNeedAsync()
        {
            return await _context.Todos
                .Select(c => new TodoNeed
                {
                    Id = c.Id,
                    Title = c.Title,
                    Deadline = c.Deadline,
                    Status = c.Status,
                    Types = c.Types,
                    Priority = c.Priority,
                    Author = c.Author,
                    UserId = c.UserId,
                    Creattime = c.Creattime,
                    Edittime = c.Edittime,
                    Needworkhour = c.Needworkhour,
                    Schedule = c.Schedule
                })
                .ToListAsync();
        }
        
        public async Task<IEnumerable<TodoNeed>> GetByUserAsync(int? userid)
        {
            return await _context.Todos
                .Where(c => c.UserId == userid)
                .Select(c => new TodoNeed
                {
                    Id = c.Id,
                    Title = c.Title,
                    Deadline = c.Deadline,
                    Status = c.Status,
                    Types = c.Types,
                    Priority = c.Priority,
                    Author = c.Author,
                    UserId = c.UserId,
                    Creattime = c.Creattime,
                    Edittime = c.Edittime,
                    Needworkhour = c.Needworkhour,
                    Schedule = c.Schedule
                })
                .ToListAsync();
        }

        public async Task<Todo> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var entity = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<bool> CreateAsync(Todo entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditAsync(Todo entity)
        {
            try
            {
                _context.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(entity.Id))
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

            var entity = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return false;
            }

            _context.Todos.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        private bool TodoExists(int? id)
        {
            return _context.Todos.Any(m => m.Id == id);
        }
    }
}