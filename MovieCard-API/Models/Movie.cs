using System.ComponentModel.DataAnnotations;

namespace MovieCard_API.Models;

public class Movie
{
    public int Id { get; set; }
    public int? DirectorId { get; set; }
    public Director? Director { get; set; }
    public ICollection<Actor>? Actors { get; set; }
    public ICollection<Genre>? Genres { get; set; }
    [StringLength(30)] public string Title { get; set; }
    [Range(0, 10)] public string Rating { get; set; }
    [DataType(DataType.Date)] public DateTime ReleaseDate { get; set; }
    [StringLength(100)] public string Description { get; set; }
}