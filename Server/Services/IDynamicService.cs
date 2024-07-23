using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public interface IDynamicService
    {
        Task<IEnumerable<Dynamic>> SelectAllAsync();
        Task<Dynamic> DetailsAsync(int? id);
        Task<bool> CreateAsync(Dynamic entity);
        Task<bool> EditAsync(Dynamic entity);
        Task<bool> DeleteAsync(int? id);
    }
}