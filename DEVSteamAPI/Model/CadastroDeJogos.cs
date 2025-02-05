namespace DEVSteamAPI.Model
{
    public class CadastroDeJogos
    {
        public Guid CadastroDeJogosId { get; set; }
        public string Name { get; set; }
        public string Classificacao { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public Guid CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
