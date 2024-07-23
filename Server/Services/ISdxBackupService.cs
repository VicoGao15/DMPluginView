using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public interface ISdxBackupService
    {
        Task<IEnumerable<SdxBackupNeed>> SelectAllAsync();
        Task<IEnumerable<SdxBackup>> SelectAllBysdxbackupIdAsync(int? conceptid);
        Task<SdxBackup> DetailsAsync(int? id);
        Task<bool> CreateAsync(SdxBackup entity);
        Task<bool> EditAsync(SdxBackup entity);
        Task<bool> DeleteAsync(int? id);
    }
}