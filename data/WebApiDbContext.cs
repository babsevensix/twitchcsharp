
using Microsoft.EntityFrameworkCore;


public class WebApiDbContext : DbContext
{
    public WebApiDbContext(DbContextOptions<WebApiDbContext> options): base(options)
    {

    }
    
    public DbSet<PersonaEntity> Persone { get; set; }

    public DbSet<CittaEntity> Citta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PersonaEntity>().ToTable("Persone");
        modelBuilder.Entity<PersonaEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<PersonaEntity>().Property(x => x.Id).ValueGeneratedOnAdd();
    }
}