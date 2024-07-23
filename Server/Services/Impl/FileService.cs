using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public class FileService : IFileService
    {
        private readonly vkspDbContext _context;

        public FileService(vkspDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<File>> SelectAllAsync()
        {
            return await _context.File.ToListAsync();
        }
        
        public async Task<IEnumerable<File>> SelectAllByConceptIdAsync(int? conceptid)
        {
            return await _context.File
                .Where(c=>c.Conceptid ==conceptid)
                .ToListAsync();
        }

        public async Task<File> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var entity = await _context.File
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<bool> CreateAsync(File entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditAsync(File entity)
        {
            try
            {
                _context.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(entity.Id))
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

            var entity = await _context.File
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return false;
            }

            _context.File.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        private bool CommentExists(int? id)
        {
            return _context.File.Any(m => m.Id == id);
        }
    }
}