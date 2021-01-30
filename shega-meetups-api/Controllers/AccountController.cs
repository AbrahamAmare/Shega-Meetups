using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shega_meetups_api.Data;
using shega_meetups_api.DTOs;
using shega_meetups_api.Entities;
using shega_meetups_api.Interfaces;

namespace shega_meetups_api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;

        }

        [HttpPost("register")]

        // public async Task<ActionResult<User>> Register(string email, string password)

        // Use DTO to accept properties in the response body as an Objject
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.Email))
            {
                return BadRequest("A user with this email has already registerd !");
            }

            using var hmac = new HMACSHA512();
            var user = new User
            {
                Email = registerDTO.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        
        [HttpPost("login")]

        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == loginDTO.Email);

            if (user == null) return Unauthorized("Invalid Email");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDTO
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };

        }

        //  Helper Functions

        // Check if user with this email is already registerd
        private async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }
    }
}