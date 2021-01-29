using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shega_meetups_api.Data;
using shega_meetups_api.Entities;

namespace shega_meetups_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;

        }


        // Get a list of users from the database

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
            // return users;
        }

        // Get a specific(single) users from the database

        [HttpGet("{id}")]
        public async Task< ActionResult<User>> GetUser(int id )
        {
            var user = await _context.Users.FindAsync(id);
            return  user;
        }
    }
}