using System.ComponentModel.DataAnnotations;
using MovieCard_API.Custom_attributes;

namespace MovieCard_API.Models;

public class Movie
{
    public int Id { get; set; }
    public int? DirectorId { get; set; }
    public Director? Director { get; set; }
    public ICollection<Actor>? Actors { get; set; }
    public ICollection<Genre>? Genres { get; set; }
    [StringLength(30)] public string Title { get; set; } = string.Empty;
    [Range(0, 10)] public int Rating { get; set; }

    [DataType(DataType.Date)]
    [IsDateSetInFuture]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime ReleaseDate { get; set; }

    [StringLength(100)] public string Description { get; set; } = string.Empty;
}