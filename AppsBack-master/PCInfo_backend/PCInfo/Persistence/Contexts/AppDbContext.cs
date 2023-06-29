using Microsoft.EntityFrameworkCore;
using PCInfo_backend.PCInfo.Domain.Models;

namespace PCInfo_backend.PCInfo.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Ram> Rams { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Ram>().ToTable("rams");
        builder.Entity<Ram>().HasKey(p => p.id);
        builder.Entity<Ram>().Property(p => p.id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().ToTable("users");
        builder.Entity<User>().HasKey(p => p.idUser);
        builder.Entity<User>().Property(p => p.idUser).IsRequired().ValueGeneratedOnAdd();
       
        
      
    }
}