using APIFinanceira.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIFinanceira.Data.Mappings
{
    public class TransacaoMap : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Data)
                .IsRequired();

            builder.Property(t => t.Tipo)
                .IsRequired();

            builder.HasOne(t => t.Usuario)
                 .WithMany(u => u.Transacoes)
                 .HasForeignKey(t => t.UsuarioId);
        }
    }
}
