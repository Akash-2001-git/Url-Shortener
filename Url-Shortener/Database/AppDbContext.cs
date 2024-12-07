using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Url_Shortener.Model;

namespace Url_Shortener.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<ShortenUrl> ShortenUrl { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenUrl>
                (
                entity =>
                {
                    var converter = new ValueConverter<Guid, string>(convert => convert.ToString(), convert => new Guid(convert));
                    entity.ToTable("ShortenUrl");
                    entity.HasKey(x => x.Id);
                    entity.Property(x => x.Id).HasConversion(converter).IsRequired().HasColumnName("Id");
                    entity.Property(x => x.LongUrl).IsUnicode(false).HasColumnName("LongUrl").HasColumnType("varchar").HasMaxLength(120);
                    entity.Property(x => x.UniqueCode).IsUnicode(false).HasColumnName("UniqueCode").HasColumnType("varchar").HasMaxLength(120);
                    entity.Property(x => x.ShortUrl).IsUnicode(false).HasColumnName("ShortUrl").HasColumnType("varchar").HasMaxLength(120);
                    entity.Property(x => x.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime");
                    entity.Property(x => x.ExpirationDate).HasColumnName("ExpirationDate").HasColumnType("datetime");
                    entity.HasIndex(p => p.UniqueCode)
                    .IsUnique();
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
