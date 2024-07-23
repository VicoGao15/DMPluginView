using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public interface IFileService
    {
        Task<IEnumerable<File>> SelectAllAsync();
        Task<IEnumerable<File>> SelectAllByConceptIdAsync(int? conceptid);
        Task<File> DetailsAsync(int? id);
        Task<bool> CreateAsync(File entity);
        Task<bool> EditAsync(File entity);
        Task<bool> DeleteAsync(int? id);
    }
}