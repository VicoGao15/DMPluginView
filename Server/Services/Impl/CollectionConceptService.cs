using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public class CollectionConceptService : ICollectionConceptService
    {
        private readonly vkspDbContext _context;

        public CollectionConceptService(vkspDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<CollectionConcept>> SelectAllAsync()
        {
            return await _context.CollectionConcepts.ToListAsync();
        }
        
        public async Task<IEnumerable<CollectionConceptNeed>> SelectAllCollectionAsync(int? userid)
        {
            
            return await _context.CollectionConcepts
                .Where(c => c.UserId == userid)
                .Include(c=>c.LikeConcept)
                .Include(c=>c.LikeUser)
                .Select(c=> new CollectionConceptNeed
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    ConceptId = c.ConceptId,
                    LikeConcept = new ConceptNeed
                    {
                        Id = c.LikeConcept.Id,
                        Module = c.LikeConcept.Module,
                        Creater = c.LikeConcept.Creater,
                        Create_Date = c.LikeConcept.Create_Date,
                        Header = c.LikeConcept.Header,
                        CreaterId = c.LikeConcept.CreaterId,
                        UserInfo = null
                    },
                    LikeUser = c.LikeUser
                })
                .ToListAsync();
        }

        public async Task<bool> CreateAsync(CollectionConcept entity)
        {
            if (_context.CollectionConcepts.Any(
                o => o.ConceptId == entity.ConceptId &&
                o.UserId == entity.UserId)) 
            {
                return true;
            }
            _context.Add(entity);
            
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<bool> CheckAsync(int? userid, int? conceptid)
        {
            if (_context.CollectionConcepts.Any(
                o => o.ConceptId == conceptid &&
                o.UserId == userid))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int? userid, int? conceptid)
        {
            if (userid == null || conceptid == null)
            {
                return false;
            }

            var deleteEntity = await _context.CollectionConcepts
                .FirstOrDefaultAsync(m => m.UserId == userid && m.ConceptId == conceptid);

            _context.CollectionConcepts.Remove(deleteEntity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}