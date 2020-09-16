using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Entities
{
    public partial class RepositoryContext : DbContext
    {
        public RepositoryContext()
        {
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<GeoMetric> GeoMetric { get; set; }
        public virtual DbSet<Run> Run { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Geometrics;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CityName).IsRequired();

                entity.Property(e => e.Url).IsRequired();
            });

            modelBuilder.Entity<GeoMetric>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DayName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsFixedLength();

                entity.Property(e => e.DayNumber)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsFixedLength();

                entity.Property(e => e.FkRunId).HasColumnName("FK_RunId");

                entity.Property(e => e.KmH).IsRequired();

                entity.Property(e => e.MaxTempC).IsRequired();

                entity.Property(e => e.MaxTempF).IsRequired();

                entity.Property(e => e.MiH).IsRequired();

                entity.Property(e => e.MinTempC).IsRequired();

                entity.Property(e => e.MinTempF).IsRequired();

                entity.Property(e => e.Prec).IsRequired();

                entity.Property(e => e.WindMs).IsRequired();

                entity.HasOne(d => d.FkRun)
                    .WithMany(p => p.GeoMetric)
                    .HasForeignKey(d => d.FkRunId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeoMetric_Run");
            });

            modelBuilder.Entity<Run>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EndedAt).HasColumnType("datetime");

                entity.Property(e => e.FkCity).HasColumnName("FK_City");

                entity.Property(e => e.StartedAt).HasColumnType("datetime");

                entity.HasOne(d => d.FkCityNavigation)
                    .WithMany(p => p.Run)
                    .HasForeignKey(d => d.FkCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Run_City");
            });
        }
    }
}
