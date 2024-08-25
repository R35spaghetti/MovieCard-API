using Microsoft.EntityFrameworkCore;
using MovieCard_API.Models;

namespace MovieCard_API.Data;

public class MovieCardContext(DbContextOptions<MovieCardContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Director)
            .WithMany(d => d.Movies);

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


        modelBuilder.Entity<MovieActor>()
            .HasKey(ma => new { ma.MovieId, ma.ActorId });

        modelBuilder.Entity<MovieActor>()
            .HasOne(ma => ma.Movie)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(ma => ma.MovieId);

        modelBuilder.Entity<MovieActor>()
            .HasOne(ma => ma.Actor)
            .WithMany(a => a.MovieActors)
            .HasForeignKey(ma => ma.ActorId);
    }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<ContactInformation> ContactInformations => Set<ContactInformation>();
    public DbSet<Actor> Actors => Set<Actor>();
}