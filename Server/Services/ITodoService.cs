using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> SelectAllAsync();
        Task<IEnumerable<TodoNeed>> GetAllNeedAsync();
        Task<IEnumerable<TodoNeed>> GetByUserAsync(int? userid);
        Task<Todo> DetailsAsync(int? id);
        Task<bool> CreateAsync(Todo entity);
        Task<bool> EditAsync(Todo entity);
        Task<bool> DeleteAsync(int? id);
    }
}