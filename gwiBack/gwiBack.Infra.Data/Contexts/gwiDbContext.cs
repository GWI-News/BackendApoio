using gwiBack.Domain.Entities; // Adicione esta linha para incluir as entidades
using Microsoft.EntityFrameworkCore;

namespace gwiBack.Infra.Data.Contexts
{
    public class gwiDbContext : DbContext
    {
        // Construtor
        public gwiDbContext(DbContextOptions<gwiDbContext> options) : base(options) { }

        // DbSets para as entidades
        public DbSet<Formation> Formations { get; set; }
        public DbSet<ProfessionalInformation> ProfessionalInformation { get; set; }
        public DbSet<ProfessionalSkill> ProfessionalSkills { get; set; }

        // Configurações de modelagem
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
