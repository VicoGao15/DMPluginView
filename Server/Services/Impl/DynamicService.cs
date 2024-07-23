using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public class DynamicService : IDynamicService
    {
        private readonly vkspDbContext _context;

        public DynamicService(vkspDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Dynamic>> SelectAllAsync()
        {
            return await _context.Dynamics.ToListAsync();
        }

        public async Task<Dynamic> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var entity = await _context.Dynamics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<bool> CreateAsync(Dynamic entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditAsync(Dynamic entity)
        {
            try
            {
                _context.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DynamicExists(entity.Id))
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

            var entity = await _context.Dynamics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return false;
            }

            _context.Dynamics.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        private bool DynamicExists(int? id)
        {
            return _context.Dynamics.Any(m => m.Id == id);
        }
    }
}