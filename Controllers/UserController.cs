using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Version2SAMart.Data;
using Version2SAMart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Version2SAMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Version2SAMartContext Context;

        public UsersController(Version2SAMartContext context)
        {
            Context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await Context.Users.FindAsync(id);
            if (user == null) { return NotFound(); }
            return user;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() => await Context.Users.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id) {return BadRequest();}

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await Context.Users.FindAsync(id);
            if (user == null) {return NotFound();}

            Context.Users.Remove(user);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        {
            Console.WriteLine($"Attempting login for user: {username}");

            var user = await Context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null && user.Password == password)
            {
                HttpContext.Session.SetString("UserRole", "User");
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
