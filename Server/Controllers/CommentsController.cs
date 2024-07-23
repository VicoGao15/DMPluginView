using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6;

namespace Net6
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _service;

        public CommentsController(ICommentsService service)
        {
            _service = service;
        }

        // GET: api/<CommentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.SelectAllAsync());
        }

    }
}