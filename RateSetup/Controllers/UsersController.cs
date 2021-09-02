using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateSetup.Helpers;
using RateSetup.Models.Authentication;
using RateSetup.Models.Database;
using RateSetup.Models.UserServiceModels;
using RateSetup.Services.UserService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RateSetup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(EFContext context, IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _userService.GetAll();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await _userService.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _userService.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users/registration
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("registration")]
        public async Task<ActionResult<User>> PostUser(RegisterUserRequest user)
        {
            var id = await _userService.Register(user);

            return CreatedAtAction("GetUser", new { id = id }, user);
        }

        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.Delete(user);

            return NoContent();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.AuthenticateAsync(model);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }
    }
}
