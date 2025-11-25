using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrgCore.Domain.Contexts.Identity.Entities;

namespace OrgCore.Infrastructure.Mappings
{
    public class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresas");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.NomeFantasia)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Cnpj)
                .IsRequired()
                .HasColumnType("varchar(14)");
        }
    }
}
