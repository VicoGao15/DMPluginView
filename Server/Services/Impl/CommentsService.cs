using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net6;

namespace Net6
{
    public class CommentsService : ICommentsService
    {
        private readonly vkspDbContext _context;

        public CommentsService(vkspDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comments>> SelectAllAsync()
        {
            return await _context.CommentNew
                .Include(c=>c.ReplyComment)
                //.Include(c=>c.Up_User)
                //.Include(c=>c.Down_User)
                .ToListAsync();
        }
    }
}