using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6;

namespace Net6
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        // GET: api/<CommentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.SelectAllAsync());
        }

        // GET: api/<CommentController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _service.DetailsAsync(id));
        }

        // GET: api/<CommentController>/id
        [HttpGet("concepts/{conceptid}")]
        public async Task<IActionResult> GetbyConceptId(int? conceptid)
        {
            return Ok(await _service.SelectAllByConceptIdAsync(conceptid));
        }

        // POST: api/<CommentController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Comment entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.CreateAsync(entity));
            }
            return NotFound();
        }
        
        // Put: api/<CommentController>
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Comment entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.EditAsync(entity));
            }
            return NotFound();
        }

        // DELETE: api/<CommentController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}