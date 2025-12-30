using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrgCore.Domain.Evaluation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Infrastructure.Mappings
{
    public class AvaliacaoMap : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.ToTable("Avaliacoes");
            builder.HasKey(a => a.Id);

            builder.HasMany(a => a.Respostas)
                   .WithOne()
                   .HasForeignKey("AvaliacaoId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
