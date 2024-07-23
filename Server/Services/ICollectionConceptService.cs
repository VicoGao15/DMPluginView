using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public interface ICollectionConceptService
    {
        Task<IEnumerable<CollectionConcept>> SelectAllAsync();
        Task<IEnumerable<CollectionConceptNeed>> SelectAllCollectionAsync(int? userid);
        Task<bool> CreateAsync(CollectionConcept entity);
        Task<bool> CheckAsync(int? userid, int? conceptid);
        Task<bool> DeleteAsync(int? userid, int? conceptid);
    }
}