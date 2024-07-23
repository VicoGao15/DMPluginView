using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public class SdxBackupService : ISdxBackupService
    {
        private readonly vkspDbContext _context;

        public SdxBackupService(vkspDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<SdxBackupNeed>> SelectAllAsync()
        {
            //return await _context.SdxBackups.ToListAsync();
            return await _context.SdxBackups
                .Select(c => new SdxBackupNeed
                {
                    Id = c.Id,
                    Build = c.Build,
                    Version = c.Version,
                    IsKeyVersion = c.IsKeyVersion,
                    KeyVersion = c.KeyVersion,
                    Remark = c.Remark,
                    Offlineinstaller = c.Offlineinstaller
                })
                .ToListAsync();
        }
        
        public async Task<IEnumerable<SdxBackup>> SelectAllBysdxbackupIdAsync(int? conceptid)
        {
            return await _context.SdxBackups
                .Where(c=>c.Id ==conceptid)
                .ToListAsync();
        }

        public async Task<SdxBackup> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var entity = await _context.SdxBackups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<bool> CreateAsync(SdxBackup entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditAsync(SdxBackup entity)
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

            var entity = await _context.SdxBackups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return false;
            }

            _context.SdxBackups.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        private bool CommentExists(int? id)
        {
            return _context.SdxBackups.Any(m => m.Id == id);
        }
    }
}