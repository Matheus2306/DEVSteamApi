using System.ComponentModel.DataAnnotations;

namespace DEVSteamAPI.Model
{
    public class ItemCarrinho
    {
        public Guid ItemCarrinhoId { get; set; }
        public Guid CarrinhoId { get; set; }
        public Guid CadastroDeJogosId { get; set; }
        [Required(ErrorMessage = "O campo quantidade é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }
        [Display(Name = "Valor")]
        [Required(ErrorMessage = "O campo valor é obrigatório")]
        [Range(0.1, 99999.99, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Valor { get; set; }
        public decimal total { get; set; }
    }
}
