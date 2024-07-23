using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6;
using System.Diagnostics;

namespace Net6
{
    public class ConceptService : IConceptService
    {
        private readonly vkspDbContext _context;
        
        public ConceptService(vkspDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Concept>> SelectAllAsync()
        {
            return await _context.Concepts.ToListAsync();
        }
        
        public async Task<IEnumerable<ConceptNeed>> GetAllNeedAsync()
        {
            return await _context.Concepts
                .Include(c=>c.UserInfo)
                .Select(c => new ConceptNeed 
                {
                    Id=c.Id,
                    Module=c.Module,
                    Creater=c.Creater,
                    Create_Date=c.Create_Date,
                    Header=c.Header,
                    CreaterId=c.CreaterId,
                    UserInfo=c.UserInfo,
                    IsPrivate=c.IsPrivate ==1?true:false,
                    SimpleContent=c.Contents.Substring(0,100)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ConceptNeed>> GetMyConceptNeedAsync(int? userid)
        {
            return await _context.Concepts
                .Where(e => e.CreaterId == userid)
                .Select(c => new ConceptNeed
                {
                    Id = c.Id,
                    Module = c.Module,
                    Creater = c.Creater,
                    Create_Date = c.Create_Date,
                    Header = c.Header,
                    CreaterId = c.CreaterId,
                    UserInfo = c.UserInfo
                })
                .OrderByDescending(e => e.Create_Date)
                .ToListAsync();
        }

        public async Task<Concept> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var watch = new Stopwatch();

            watch.Start();

            var entity = await _context.Concepts
                .Include(c=>c.UserInfo)
                .FirstOrDefaultAsync(m => m.Id == id);

            watch.Stop();
            
            System.Console.WriteLine(watch.ElapsedMilliseconds);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }
        
        public async Task<ConceptNeed> GetConceptwithoutContentAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var entity = await _context.Concepts
                .Include(c => c.UserInfo)
                .Select(c => new ConceptNeed
                {
                    Id = c.Id,
                    Module = c.Module,
                    Creater = c.Creater,
                    Create_Date = c.Create_Date,
                    Header = c.Header,
                    CreaterId = c.CreaterId,
                    UserInfo = c.UserInfo
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }
        
        public async Task<ConceptContent> GetContentDetailsByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var entity = await _context.Concepts
                .Include(c => c.UserInfo)
                .Select(c => new ConceptContent
                {
                    Id = c.Id,
                    Contents = c.Contents,
                    Likes = c.Likes,
                    Views = c.Views,
                    UserInfo = c.UserInfo
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<int> CreateAsync(Concept entity)
        {
            Console.WriteLine(entity);
            _context.Add(entity);
            int affectedRow = await _context.SaveChangesAsync();
            _context.Entry(entity);
            
            return affectedRow>0 ? entity.Id : 0;
        }
        

        public async Task<bool> EditAsync(Concept entity)
        {
            try
            {
                _context.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConceptExists(entity.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        //public async Task<bool> UpdateCulumnAsync(int conceptid, int isprivate) 
        //{
        //    // 创建一个匿名对象，指定要更新的列和新值
        //    var updatedData = new { isprivate = isprivate };

        //    // 使用EF Core的Update方法更新数据
        //    _context.Concepts.Update(updatedData, x => x.Id == conceptid);
        //    _context.SaveChanges();

        //    // 创建一个实体的包装器
        //    var entity = new Concept { Id = conceptid };

        //    // 将要更新的列的值设置为新值
        //    _context.Entry(Concept).Property(x => x.IsPrivate).CurrentValue = isprivate;

        //    // 将实体的状态标记为已修改
        //    _context.Entry(entity).State = EntityState.Modified;

        //    // 保存更改到数据库
        //    _context.SaveChanges();
        //    return false;
        //}

        public async Task<bool> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return false;
            }

            var entity = await _context.Concepts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return false;
            }

            _context.Concepts.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        private bool ConceptExists(int? id)
        {
            return _context.Concepts.Any(m => m.Id == id);
        }
    }
}