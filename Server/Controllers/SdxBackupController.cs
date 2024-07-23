using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6;

namespace Net6
{
    [Route("api/[controller]")]
    [ApiController]
    public class SdxBackupController : ControllerBase
    {
        private readonly ISdxBackupService _service;

        public SdxBackupController(ISdxBackupService service)
        {
            _service = service;
        }

        // GET: api/<SdxBackupController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.SelectAllAsync());
        }

        // GET: api/<SdxBackupController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _service.DetailsAsync(id));
        }

        // GET: api/<SdxBackupController>/id
        [HttpGet("sdxbackups/{sdxbackupid}")]
        public async Task<IActionResult> GetbysdxbackupId(int? sdxbackupid)
        {
            return Ok(await _service.SelectAllBysdxbackupIdAsync(sdxbackupid));
        }

        // POST: api/<SdxBackupController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SdxBackup entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.CreateAsync(entity));
            }
            return NotFound();
        }
        
        // Put: api/<SdxBackupController>
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] SdxBackup entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.EditAsync(entity));
            }
            return NotFound();
        }

        // DELETE: api/<SdxBackupController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}