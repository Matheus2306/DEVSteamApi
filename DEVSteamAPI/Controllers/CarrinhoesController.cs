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
    public class CarrinhoesController : ControllerBase
    {
        private readonly DEVsteamAPIContext _context;

        // Construtor que inicializa o contexto do banco de dados
        public CarrinhoesController(DEVsteamAPIContext context)
        {
            _context = context;
        }

        // GET: api/Carrinhoes
        // Retorna todos os carrinhos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrinho>>> GetCarrinho()
        {
            return await _context.Carrinho.ToListAsync();
        }

        // GET: api/Carrinhoes/5
        // Retorna um carrinho específico pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrinho>> GetCarrinho(Guid id)
        {
            var carrinho = await _context.Carrinho.FindAsync(id);

            if (carrinho == null)
            {
                return NotFound();
            }

            return carrinho;
        }

        // GET: api/Carrinhoes/bydate
        // Retorna carrinhos criados dentro de um intervalo de datas
        [HttpGet("bydate")]
        public async Task<ActionResult<IEnumerable<Carrinho>>> GetCarrinhoByDate(DateTime startDate, DateTime endDate)
        {
            var carrinhos = await _context.Carrinho
                .Where(c => c.DataCriacao >= startDate && c.DataCriacao <= endDate)
                .ToListAsync();

            if (carrinhos == null || !carrinhos.Any())
            {
                return NotFound();
            }

            return carrinhos;
        }

        // PUT: api/Carrinhoes/5
        // Atualiza um carrinho existente pelo ID
        // Para proteger contra ataques de overposting, veja https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrinho(Guid id, Carrinho carrinho)
        {
            if (id != carrinho.CarrinhoID)
            {
                return BadRequest();
            }

            _context.Entry(carrinho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrinhoExists(id))
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

        // POST: api/Carrinhoes
        // Cria um novo carrinho
        // Para proteger contra ataques de overposting, veja https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carrinho>> PostCarrinho(Carrinho carrinho)
        {
            _context.Carrinho.Add(carrinho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrinho", new { id = carrinho.CarrinhoID }, carrinho);
        }

        // DELETE: api/Carrinhoes/5
        // Deleta um carrinho existente pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrinho(Guid id)
        {
            var carrinho = await _context.Carrinho.FindAsync(id);
            if (carrinho == null)
            {
                return NotFound();
            }

            _context.Carrinho.Remove(carrinho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar se um carrinho existe pelo ID
        private bool CarrinhoExists(Guid id)
        {
            return _context.Carrinho.Any(e => e.CarrinhoID == id);
        }
    }
}
