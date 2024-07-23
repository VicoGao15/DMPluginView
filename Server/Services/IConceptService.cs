using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public interface IConceptService
    {
        Task<IEnumerable<Concept>> SelectAllAsync();
        Task<IEnumerable<ConceptNeed>> GetAllNeedAsync();
        Task<IEnumerable<ConceptNeed>> GetMyConceptNeedAsync(int? userid);
        Task<Concept> DetailsAsync(int? id);
        Task<ConceptContent> GetContentDetailsByIdAsync(int? id);
        Task<ConceptNeed> GetConceptwithoutContentAsync(int? id);
        Task<int> CreateAsync(Concept entity);
        Task<bool> EditAsync(Concept entity);
        Task<bool> DeleteAsync(int? id);
    }
}