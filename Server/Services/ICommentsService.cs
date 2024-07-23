using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public interface ICommentsService
    {
        Task<IEnumerable<Comments>> SelectAllAsync();
    }
}