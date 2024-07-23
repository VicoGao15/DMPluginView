using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6;

namespace Net6
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionConceptController : ControllerBase
    {
        private readonly ICollectionConceptService _service;

        public CollectionConceptController(ICollectionConceptService service)
        {
            _service = service;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.SelectAllAsync());
        }

        // GET: api/<UserController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserId(int? id)
        {
            return Ok(await _service.SelectAllCollectionAsync(id));
        }

        // POST: api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CollectionConcept entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.CreateAsync(entity));
            }
            return NotFound();
        }

        // Get: api/<UserController>
        [HttpGet("checkifcollection")]
        public async Task<IActionResult> CheckIfCollection(int? userid,int? conceptid)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.CheckAsync(userid, conceptid));
            }
            return NotFound();
        }
        

        // DELETE: api/<UserController>
        [HttpDelete("deleteofcollection")]
        public async Task<IActionResult> Delete(int? userid, int? conceptid)
        {
            return Ok(await _service.DeleteAsync(userid, conceptid));
        }
    }
}