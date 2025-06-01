using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIrest_DAD.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIrest_DAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificacaosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotificacaosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Notificacaos
        [HttpGet]
        public async Task<dynamic> Getnotificacao(int page = 1, int pageSize = 3)
        {
            var notificacoes = await _context.notificacao
                .Skip((page-1)*page)
                .Take(pageSize)
                .ToListAsync();

            return new
            {
                data = notificacoes,
                currentPage = page,
                pageSize = pageSize,
                total = await _context.notificacao.CountAsync()
            };
        }

        // GET: api/Notificacaos/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Notificacao>> GetNotificacao(int id)
        {
            var notificacao = await _context.notificacao.FindAsync(id);

            if (notificacao == null)
            {
                return NotFound();
            }

            return notificacao;
        }

        // PUT: api/Notificacaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutNotificacao(int id, Notificacao notificacao)
        {
            if (id != notificacao.codigoNotificacao)
            {
                return BadRequest();
            }

            _context.Entry(notificacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificacaoExists(id))
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

        // POST: api/Notificacaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Notificacao>> PostNotificacao(Notificacao notificacao)
        {
            _context.notificacao.Add(notificacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotificacao", new { id = notificacao.codigoNotificacao }, notificacao);
        }

        // DELETE: api/Notificacaos/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteNotificacao(int id)
        {
            var notificacao = await _context.notificacao.FindAsync(id);
            if (notificacao == null)
            {
                return NotFound();
            }

            _context.notificacao.Remove(notificacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificacaoExists(int id)
        {
            return _context.notificacao.Any(e => e.codigoNotificacao == id);
        }
    }
}
