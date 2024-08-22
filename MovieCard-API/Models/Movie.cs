using System.Collections.ObjectModel;

namespace MovieCard_API.Models;

public class Movie
{
    public int Id { get; set; }
    public int DirectorId { get; set; }
    public Director Director { get; set; }
    public Collection<Actor> Actors { get; set; }
    public Collection<Genre> Genres { get; set; }
    public string Title { get; set; }
    public string Rating { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; }
}