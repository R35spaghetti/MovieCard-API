using Microsoft.EntityFrameworkCore;
using MovieCard_API.Models;

namespace MovieCard_API.Data;

public class MovieCardContext(DbContextOptions<MovieCardContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Director)
            .WithMany(d=>d.Movies);

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Actors)
            .WithMany(m => m.Movies);

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Genres)
            .WithMany(m => m.Movies);

        modelBuilder.Entity<Director>()
            .HasOne(m => m.ContactInformation)
            .WithOne(d=>d.Director)
            .HasForeignKey<ContactInformation>(co => co.DirectorId);


    }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<ContactInformation> ContactInformations => Set<ContactInformation>();
    public DbSet<Actor> Actors => Set<Actor>();

}