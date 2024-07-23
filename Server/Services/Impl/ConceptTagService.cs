using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public class ConceptTagService : IConceptTagService
    {
        private readonly vkspDbContext _context;

        public ConceptTagService(vkspDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<ConceptTag>> SelectAllAsync()
        {
            return await _context.ConceptTags.ToListAsync();
        }

        public async Task<bool> CreateAsync(ConceptTag entity)
        {
            if (_context.ConceptTags.Any(
                o => o.ConceptId == entity.ConceptId &&
                o.TagId == entity.TagId)) 
            {
                return true;
            }
            _context.Add(entity);
            
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int? tagid, int? conceptid)
        {
            if (tagid == null || conceptid == null)
            {
                return false;
            }

            var deleteEntity = await _context.ConceptTags
                .FirstOrDefaultAsync(m => m.TagId == tagid && m.ConceptId == conceptid);

            _context.ConceptTags.Remove(deleteEntity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}