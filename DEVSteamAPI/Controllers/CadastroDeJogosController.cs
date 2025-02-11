using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DEVSteamAPI.Data;
using DEVSteamAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace DEVSteamAPI.Controllers
{
    //[AllowAnonymous] serve pra liberar acesso a uma função especifica ou permitir acesso total a todos
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CadastroDeJogosController : ControllerBase
    {
        private readonly DEVsteamAPIContext _context;

        public CadastroDeJogosController(DEVsteamAPIContext context)
        {
            _context = context;
        }

        // GET: api/CadastroDeJogos
        // Retorna todos os jogos cadastrados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CadastroDeJogos>>> GetCadastroDeJogos()
        {
            return await _context.CadastroDeJogos.ToListAsync();
        }

        // GET: api/CadastroDeJogos/5
        // Retorna um jogo específico pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CadastroDeJogos>> GetCadastroDeJogos(Guid id)
        {
            var cadastroDeJogos = await _context.CadastroDeJogos.FindAsync(id);

            if (cadastroDeJogos == null)
            {
                return NotFound();
            }

            return cadastroDeJogos;
        }

        // GET: api/CadastroDeJogos/byname
        // Retorna jogos que correspondem ao nome fornecido
        [HttpGet("byname")]
        public async Task<ActionResult<IEnumerable<CadastroDeJogos>>> GetCadastroDeJogosByName(string name)
        {
            var jogos = await _context.CadastroDeJogos
                .Where(j => j.Name.Contains(name))
                .ToListAsync();

            if (jogos == null || !jogos.Any())
            {
                return NotFound();
            }

            return jogos;
        }

        // PUT: api/CadastroDeJogos/5
        // Atualiza um jogo existente pelo ID
        // Para proteger contra ataques de overposting, veja https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCadastroDeJogos(Guid id, CadastroDeJogos cadastroDeJogos)
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
        // Cria um novo jogo
        // Para proteger contra ataques de overposting, veja https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CadastroDeJogos>> PostCadastroDeJogos(CadastroDeJogos cadastroDeJogos)
        {
            _context.CadastroDeJogos.Add(cadastroDeJogos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCadastroDeJogos", new { id = cadastroDeJogos.CadastroDeJogosId }, cadastroDeJogos);
        }

        // DELETE: api/CadastroDeJogos/5
        // Deleta um jogo existente pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCadastroDeJogos(Guid id)
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

        // Método auxiliar para verificar se um jogo existe pelo ID
        private bool CadastroDeJogosExists(Guid id)
        {
            return _context.CadastroDeJogos.Any(e => e.CadastroDeJogosId == id);
        }
    }
}
