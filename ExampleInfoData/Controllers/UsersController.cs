using ExampleInfoData.Models;
using ExampleInfoData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleInfoData.Controllers
{
    [Route("api/info/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/getinfo-mobile")]
        [Route("/GetInfoMobile")]
        public async Task<IActionResult> GetInfoMobile()
        {
            var request = HttpContext.Request;

            // Full URL
            var fullUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";

            var d = await _userService.GetAsync();
            return Ok(d);
        }


        // GET ALL
        [HttpGet("{id:int}/{department:alpha:minlength(3)?}")]
        public async Task<IActionResult> GetMyInfoData(string? department, int? id)
        {
            var d = await _userService.GetAsync();
            return Ok(d);
        }


        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetInfo()
        {
            var d= await _userService.GetAsync();
            return Ok(d);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetInfo(string id)
        {
            var user = await _userService.GetByIdAsync(id);

            var m = new { message = "not found" };

            if (user == null)
                return BadRequest(m);

            return user;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> PostInfo(User user)
        {
            await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(GetInfo),
                new { id = user.Id }, user);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfo(string id, User user)
        {
            var existingUser = await _userService.GetByIdAsync(id);

            if (existingUser == null)
                return NotFound();

            user.Id = existingUser.Id;

            await _userService.UpdateAsync(id, user);

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            await _userService.DeleteAsync(id);

            return NoContent();
        }
    }
}
