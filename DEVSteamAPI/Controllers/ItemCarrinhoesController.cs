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
    public class ItemCarrinhoesController : ControllerBase
    {
        private readonly DEVsteamAPIContext _context;

        public ItemCarrinhoesController(DEVsteamAPIContext context)
        {
            _context = context;
        }

        // GET: api/ItemCarrinhoes
        // Retorna todos os itens do carrinho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCarrinho>>> GetItemCarrinho()
        {
            return await _context.ItemCarrinho.ToListAsync();
        }

        // GET: api/ItemCarrinhoes/5
        // Retorna um item do carrinho específico pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCarrinho>> GetItemCarrinho(Guid id)
        {
            var itemCarrinho = await _context.ItemCarrinho.FindAsync(id);

            if (itemCarrinho == null)
            {
                return NotFound();
            }

            return itemCarrinho;
        }

        // GET: api/ItemCarrinhoes/calculate/5
        // Calcula o valor total de um item do carrinho pelo ID
        [HttpGet("calculate/{id}")]
        public async Task<ActionResult<decimal>> CalculateItemValue(Guid id)
        {
            var itemCarrinho = await _context.ItemCarrinho.FindAsync(id);

            if (itemCarrinho == null)
            {
                return NotFound();
            }

            var totalValue = itemCarrinho.Quantidade * itemCarrinho.Valor;
            return totalValue;
        }

        // PUT: api/ItemCarrinhoes/5
        // Atualiza um item do carrinho existente pelo ID
        // Para proteger contra ataques de overposting, veja https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCarrinho(Guid id, ItemCarrinho itemCarrinho)
        {
            if (id != itemCarrinho.ItemCarrinhoId)
            {
                return BadRequest();
            }

            _context.Entry(itemCarrinho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCarrinhoExists(id))
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

        // POST: api/ItemCarrinhoes
        // Cria um novo item do carrinho e atualiza o valor total do carrinho
        // Para proteger contra ataques de overposting, veja https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemCarrinho>> PostItemCarrinho(ItemCarrinho itemCarrinho)
        {
            _context.ItemCarrinho.Add(itemCarrinho);
            await _context.SaveChangesAsync();

            // Atualiza o valor total do carrinho
            var carrinho = await _context.Carrinho.FindAsync(itemCarrinho.CarrinhoId);
            if (carrinho != null)
            {
                carrinho.Total += itemCarrinho.Quantidade * itemCarrinho.Valor;
                _context.Entry(carrinho).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetItemCarrinho", new { id = itemCarrinho.ItemCarrinhoId }, itemCarrinho);
        }

        // DELETE: api/ItemCarrinhoes/5
        // Deleta um item do carrinho existente pelo ID e atualiza o valor total do carrinho
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemCarrinho(Guid id)
        {
            var itemCarrinho = await _context.ItemCarrinho.FindAsync(id);
            if (itemCarrinho == null)
            {
                return NotFound();
            }

            _context.ItemCarrinho.Remove(itemCarrinho);
            await _context.SaveChangesAsync();

            // Atualiza o valor total do carrinho
            var carrinho = await _context.Carrinho.FindAsync(itemCarrinho.CarrinhoId);
            if (carrinho != null)
            {
                carrinho.Total -= itemCarrinho.Quantidade * itemCarrinho.Valor;
                _context.Entry(carrinho).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        // Método auxiliar para verificar se um item do carrinho existe pelo ID
        private bool ItemCarrinhoExists(Guid id)
        {
            return _context.ItemCarrinho.Any(e => e.ItemCarrinhoId == id);
        }
    }
}
