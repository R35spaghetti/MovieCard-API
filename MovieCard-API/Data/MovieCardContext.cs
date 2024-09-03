using Microsoft.EntityFrameworkCore;
using MovieCard_API.Models;

namespace MovieCard_API.Data;

public class MovieCardContext : DbContext
{
    public MovieCardContext(DbContextOptions<MovieCardContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Director)
            .WithMany(d => d.Movies)
            .HasForeignKey(m => m.DirectorId);
        
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Actors)
            .WithMany(a => a.Movies);

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Movies);

        modelBuilder.Entity<Director>()
            .HasOne(d => d.ContactInformation)
            .WithOne(c => c.Director)
            .HasForeignKey<ContactInformation>(c => c.DirectorId)
            .IsRequired();

        modelBuilder.Entity<ContactInformation>()
            .Property(ci => ci.Id)
            .ValueGeneratedOnAdd();
    }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<ContactInformation> ContactInformations => Set<ContactInformation>();
    public DbSet<Actor> Actors => Set<Actor>();
}