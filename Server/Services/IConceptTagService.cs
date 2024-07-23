using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public interface IConceptTagService
    {
        Task<IEnumerable<ConceptTag>> SelectAllAsync();
        Task<bool> CreateAsync(ConceptTag entity);
        Task<bool> DeleteAsync(int? tagid, int? conceptid);
    }
}