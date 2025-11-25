using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrgCore.Domain.Evaluation.Entities;

namespace OrgCore.Infrastructure.Mappings
{
    public class FormularioTemplateMap : IEntityTypeConfiguration<FormularioTemplate>
    {
        public void Configure(EntityTypeBuilder<FormularioTemplate> builder)
        {
            builder.ToTable("Formularios");
            builder.HasKey(x => x.Id);

            builder.HasMany(f => f.Secoes)
                   .WithOne()
                   .HasForeignKey("FormularioId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
