using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6;

namespace Net6
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbFileController : ControllerBase
    {
        private readonly IFileService _service;

        public DbFileController(IFileService service)
        {
            _service = service;
        }

        // GET: api/<DbFileController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.SelectAllAsync());
        }

        // GET: api/<DbFileController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _service.DetailsAsync(id));
        }

        // GET: api/<DbFileController>/id
        [HttpGet("concepts/{conceptid}")]
        public async Task<IActionResult> GetbyConceptId(int? conceptid)
        {
            return Ok(await _service.SelectAllByConceptIdAsync(conceptid));
        }

        // POST: api/<DbFileController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] File entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.CreateAsync(entity));
            }
            return NotFound();
        }

        // Put: api/<DbFileController>
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] File entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.EditAsync(entity));
            }
            return NotFound();
        }

        // DELETE: api/<DbFileController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}