using BonDeCollecte.Data;
using BonDeCollecte.GenereToken.Services;
using BonDeCollecte.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonDeCollecte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private  ITokenService _tokenService  ;

        public LoginsController(ApplicationDbContext context , ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // GET: api/Logins
        [HttpGet]
        //[Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogin()
        {
            return await _context.Login.ToListAsync();
        }

        // GET: api/Logins/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Login>> GetLogin(int id)
        {
            var login = await _context.Login.FindAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            return login;
        }

        // PUT: api/Logins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin(int id, Login login)
        {
            if (id != login.Id)
            {
                return BadRequest();
            }
            _context.Entry(login).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
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

        // POST: api/Logins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin(Login login)
        {
            _context.Login.Add(login);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogin", new { id = login.Id }, login);
        }


       [HttpPost("login")]
        //public IActionResult Login([FromBody] LoginModel login)
       public async Task<ActionResult<Login>> Login(Login login)
        {
            var _LoginDto = new LoginModel();
            _LoginDto.Username = login.Username;
            _LoginDto.Password = login.PasswordHash;

            var user = _context.Login.FirstOrDefault(u => u.Username == _LoginDto.Username && u.PasswordHash == _LoginDto.Password);


            if (user == null)
            {
                return Unauthorized();
            }

            // Générer un token JWT ici si tu veux une authentification par token
            var token = _tokenService.GenerateToken(user.Username, "User");

            return Ok(new { Token = token , UserRole = user.Role});
        }

    public class LoginModel
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
            public string? Role { get; set; }
        }

        // DELETE: api/Logins/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            _context.Login.Remove(login);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.Id == id);
        }
    }
}
