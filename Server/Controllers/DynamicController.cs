using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6;

namespace Net6
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicController : ControllerBase
    {
        private readonly IDynamicService _service;

        public DynamicController(IDynamicService service)
        {
            _service = service;
        }

        // GET: api/<DynamicController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.SelectAllAsync());
        }

        // GET: api/<DynamicController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _service.DetailsAsync(id));
        }

        // POST: api/<DynamicController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Dynamic entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.CreateAsync(entity));
            }
            return NotFound();
        }
        
        // Put: api/<DynamicController>
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Dynamic entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.EditAsync(entity));
            }
            return NotFound();
        }

        // DELETE: api/<DynamicController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}