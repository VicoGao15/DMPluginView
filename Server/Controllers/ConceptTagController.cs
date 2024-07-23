using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6;

namespace Net6
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptTagController : ControllerBase
    {
        private readonly IConceptTagService _service;

        public ConceptTagController(IConceptTagService service)
        {
            _service = service;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.SelectAllAsync());
        }

        // POST: api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConceptTag entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.CreateAsync(entity));
            }
            return NotFound();
        }
        

        // DELETE: api/<UserController>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int? tagid, int? conceptid)
        {
            return Ok(await _service.DeleteAsync(tagid, conceptid));
        }
    }
}