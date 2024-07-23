using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> SelectAllAsync();
        Task<IEnumerable<Comment>> SelectAllByConceptIdAsync(int? conceptid);
        Task<Comment> DetailsAsync(int? id);
        Task<bool> CreateAsync(Comment entity);
        Task<bool> EditAsync(Comment entity);
        Task<bool> DeleteAsync(int? id);
    }
}