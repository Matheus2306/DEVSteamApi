namespace DEVSteamAPI.Model
{
    public class Carrinho
    {
        public Guid CarrinhoID { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime? DataCriacao { get; set; }
        public decimal? Total { get; set; } = 0;
        public bool? Finalizado { get; set; } = false;
    }
}
