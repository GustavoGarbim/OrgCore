using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrgCore.Domain.Contexts.Identity.Entities;

namespace OrgCore.Infrastructure.Mappings
{
    public class ColaboradorMap : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.ToTable("Colaboradores");

            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Empresa)
                .WithMany()
                .HasForeignKey(c => c.EmpresaId);

            builder.HasOne(c => c.Pessoa)
                .WithMany()
                .HasForeignKey(c => c.PessoaId);
        }
    }
}
