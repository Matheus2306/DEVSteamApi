using DEVSteamAPI.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DEVSteamAPI.Data
{
    public class DEVsteamAPIContext : IdentityDbContext
    {
        public DEVsteamAPIContext(DbContextOptions<DEVsteamAPIContext> options)
            : base(options)
        {
        }

        public DbSet<CadastroDeJogos> CadastroDeJogos { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        //sobrescre o metodo OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CadastroDeJogos>().ToTable("CadastroDeJogos");
            modelBuilder.Entity<Categoria>().ToTable("Categoria");
        }
        public DbSet<DEVSteamAPI.Model.ItemCarrinho> ItemCarrinho { get; set; } = default!;
        public DbSet<DEVSteamAPI.Model.Carrinho> Carrinho { get; set; } = default!;

    }
}
