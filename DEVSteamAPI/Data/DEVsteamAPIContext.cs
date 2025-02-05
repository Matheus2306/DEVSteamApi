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

        //sobrescre o metodo OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CadastroDeJogos> CadastroDeJogos { get; set; }
    }
}
