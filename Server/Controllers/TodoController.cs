using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6;

namespace Net6
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        // GET: api/<TodoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.SelectAllAsync());
        }

        // GET: api/<TodoController>
        [HttpGet("alltodoneed")]
        public async Task<IActionResult> GetNeed()
        {
            return Ok(await _service.GetAllNeedAsync());
        }

        // GET: api/<TodoController>
        [HttpGet("user/{userid}")]
        public async Task<IActionResult> GetByUser(int? userid)
        {
            return Ok(await _service.GetByUserAsync(userid));
        }

        // GET: api/<TodoController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _service.DetailsAsync(id));
        }

        // POST: api/<TodoController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Todo entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.CreateAsync(entity));
            }
            return NotFound();
        }
        
        // Put: api/<TodoController>
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Todo entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.EditAsync(entity));
            }
            return NotFound();
        }

        // DELETE: api/<TodoController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}