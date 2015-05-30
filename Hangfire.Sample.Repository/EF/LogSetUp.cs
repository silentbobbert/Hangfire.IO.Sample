using System.Data.Entity;

namespace Hangfire.Sample.Repository.EF
{
    public class LogSetUp : DbTableSetUp<Log> 
    {
        public override void Setup(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                .HasKey(l => l.Id)
                .Property(u => u.Id)
                .IsRequired();

            modelBuilder.Entity<Log>()
                .Property(u => u.Date)
                .IsRequired();

            modelBuilder.Entity<Log>()
                .Property(t => t.Thread)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Log>()
                .Property(t => t.Level)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Log>()
                .Property(t => t.Logger)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Log>()
                .Property(t => t.Message)
                .HasColumnType("varchar")
                .HasMaxLength(4000)
                .IsRequired();

            modelBuilder.Entity<Log>()
                .Property(t => t.Exception)
                .HasColumnType("varchar")
                .HasMaxLength(2000)
                .IsRequired();
        }
    }
}