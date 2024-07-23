using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net6;

namespace Net6
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.SelectAllAsync());
        }

        // GET: api/<UserController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _service.DetailsAsync(id));
        }

        // POST: api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.CreateAsync(entity));
            }
            return NotFound();
        }

        // POST: api/<UserController>
        [HttpPost("login")]
        public User Login(string username, string pwd)
        {
           return _service.Login(username, pwd);
        }
        
        // Put: api/<UserController>
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] User entity)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.EditAsync(entity));
            }
            return NotFound();
        }

        // DELETE: api/<UserController>
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}