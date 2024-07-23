using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6;

namespace Net6
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptController : ControllerBase
    {
        private readonly IConceptService _service;

        public ConceptController(IConceptService service)
        {
            _service = service;
        }

        // GET: api/<ConceptController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.SelectAllAsync());
        }

        // GET: api/ConceptNeed/
        [HttpGet("allneed")]
        public async Task<IActionResult> GetAllNeed()
        {
            return Ok(await _service.GetAllNeedAsync());
        }

        // GET: api/ConceptNeed/
        [HttpGet("userconcept")]
        public async Task<IActionResult> GetMyConceptNeed(int? userid)
        {
            return Ok(await _service.GetMyConceptNeedAsync(userid));
        }

        // GET: api/<ConceptController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _service.DetailsAsync(id));
        }

        // GET: api/<ConceptController>/contentdetail/id
        //只获取内容相关字段
        [HttpGet("contentdetail/{id}")]
        public async Task<IActionResult> GetDetailContent(int? id)
        {
            return Ok(await _service.GetContentDetailsByIdAsync(id));
        }

        //获取不包括内容的单个concept相关字段
        [HttpGet("withoutcontent/{id}")]
        public async Task<IActionResult> GetConceptwithoutContent(int? id)
        {
            return Ok(await _service.GetConceptwithoutContentAsync(id));
        }

        // POST: api/<ConceptController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Concept entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.CreateAsync(entity));
            }
            return NotFound();
        }
        
        // Put: api/<ConceptController>
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Concept entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.EditAsync(entity));
            }
            return NotFound();
        }

        // DELETE: api/<ConceptController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}