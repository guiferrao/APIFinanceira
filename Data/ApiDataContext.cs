using APIFinanceira.Models;
using APIFinanceira.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace APIFinanceira.Data
{
    public class ApiDataContext : DbContext
    {
        public ApiDataContext(DbContextOptions<ApiDataContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new TransacaoMap());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
