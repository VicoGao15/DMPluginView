using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public class CommentService : ICommentService
    {
        private readonly vkspDbContext _context;

        public CommentService(vkspDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Comment>> SelectAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }
        
        public async Task<IEnumerable<Comment>> SelectAllByConceptIdAsync(int? conceptid)
        {
            return await _context.Comments
                .Where(c=>c.Conceptid ==conceptid)
                .Include(c=>c.UserInfo)
                .ToListAsync();
        }

        public async Task<Comment> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var entity = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<bool> CreateAsync(Comment entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditAsync(Comment entity)
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

            var entity = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return false;
            }

            _context.Comments.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        private bool CommentExists(int? id)
        {
            return _context.Comments.Any(m => m.Id == id);
        }
    }
}