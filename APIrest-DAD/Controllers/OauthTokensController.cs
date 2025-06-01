using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIrest_DAD.Models;
using Microsoft.AspNetCore.Authorization;
using BC = BCrypt.Net.BCrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using APIrest_DAD.Models.DTO;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace APIrest_DAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OauthTokensController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OauthTokensController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OauthTokens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OauthToken>>> GetoauthToken()
        {
            return await _context.oauthToken.ToListAsync();
        }

        // GET: api/OauthTokens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OauthToken>> GetOauthToken(int id)
        {
            var oauthToken = await _context.oauthToken.FindAsync(id);

            if (oauthToken == null)
            {
                return NotFound();
            }

            return oauthToken;
        }

        // PUT: api/OauthTokens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOauthToken(int id, OauthToken oauthToken)
        {
            if (id != oauthToken.id)
            {
                return BadRequest();
            }

            oauthToken.token = BC.HashPassword(oauthToken.token);
            _context.Entry(oauthToken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OauthTokenExists(id))
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

        // POST: api/OauthTokens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OauthToken>> PostOauthToken(OauthToken oauthToken)
        {
            oauthToken.token = BC.HashPassword(oauthToken.token);
            _context.oauthToken.Add(oauthToken);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOauthToken", new { id = oauthToken.id }, oauthToken);
        }

        // DELETE: api/OauthTokens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOauthToken(int id)
        {
            var oauthToken = await _context.oauthToken.FindAsync(id);
            if (oauthToken == null)
            {
                return NotFound();
            }

            _context.oauthToken.Remove(oauthToken);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto clientLogin)
        {
            var client = await _context.oauthToken.SingleOrDefaultAsync(x => x.client == clientLogin.client);

            if (client == null || client.token != clientLogin.token)
            {
                return NotFound(new { Message = "Cliente ou token invalido"});
            }else if (client.expires_at < DateTime.Now)
            {
                return NotFound(new { Message = "Cliente com token expirado" });
            }

                var jwtToken = GenerateJwtToken(client);

            return Ok(new { jwt = jwtToken });
        }

        private string GenerateJwtToken(OauthToken client)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("DESENVOLVIMENTO-DE-APLICACOES-DISTRIBUIDAS-SISTEMA-DE-INFORMACAO-CAMPUS-CONTAGEM");
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim (ClaimTypes.NameIdentifier, client.id.ToString()),
                new Claim(ClaimTypes.Email, client.client)
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool OauthTokenExists(int id)
        {
            return _context.oauthToken.Any(e => e.id == id);
        }
    }
}
