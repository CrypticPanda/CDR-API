using Microsoft.EntityFrameworkCore;

namespace CDR_BIP.Model
{
    public class CDRContext : DbContext
    {
        public CDRContext(DbContextOptions<CDRContext> options)
        : base(options)
        {
        }

        public DbSet<CDR> CDRs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CDR>(entity =>
            {
                entity
                .ToTable("CDR")
                .HasKey(k => k.reference);
            });
        }
    }
}
