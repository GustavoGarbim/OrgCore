using Microsoft.EntityFrameworkCore;
using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Domain.Evaluation.Entities;

namespace OrgCore.Infrastructure.Context
{
    public class OrgCoreDbContext : DbContext
    {
        public OrgCoreDbContext(DbContextOptions<OrgCoreDbContext> options) : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<FormularioTemplate> Formularios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrgCoreDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}