using ExampleInfoData.Models;
using ExampleInfoData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleInfoData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return await _userService.GetAsync();
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return user;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(Get),
                new { id = user.Id }, user);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, User user)
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
