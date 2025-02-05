using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DEVSteamAPI.Data;
using DEVSteamAPI.Model;

namespace DEVSteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroDeJogosController : ControllerBase
    {
        private readonly DEVsteamAPIContext _context;

        public CadastroDeJogosController(DEVsteamAPIContext context)
        {
            _context = context;
        }

        // GET: api/CadastroDeJogos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CadastroDeJogos>>> GetCadastroDeJogos()
        {
            return await _context.CadastroDeJogos.ToListAsync();
        }

        // GET: api/CadastroDeJogos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CadastroDeJogos>> GetCadastroDeJogos(int id)
        {
            var cadastroDeJogos = await _context.CadastroDeJogos.FindAsync(id);

            if (cadastroDeJogos == null)
            {
                return NotFound();
            }

            return cadastroDeJogos;
        }

        // PUT: api/CadastroDeJogos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCadastroDeJogos(int id, CadastroDeJogos cadastroDeJogos)
        {
            if (id != cadastroDeJogos.CadastroDeJogosId)
            {
                return BadRequest();
            }

            _context.Entry(cadastroDeJogos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CadastroDeJogosExists(id))
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

        // POST: api/CadastroDeJogos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CadastroDeJogos>> PostCadastroDeJogos(CadastroDeJogos cadastroDeJogos)
        {
            _context.CadastroDeJogos.Add(cadastroDeJogos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCadastroDeJogos", new { id = cadastroDeJogos.CadastroDeJogosId }, cadastroDeJogos);
        }

        // DELETE: api/CadastroDeJogos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCadastroDeJogos(int id)
        {
            var cadastroDeJogos = await _context.CadastroDeJogos.FindAsync(id);
            if (cadastroDeJogos == null)
            {
                return NotFound();
            }

            _context.CadastroDeJogos.Remove(cadastroDeJogos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CadastroDeJogosExists(int id)
        {
            return _context.CadastroDeJogos.Any(e => e.CadastroDeJogosId == id);
        }
    }
}
