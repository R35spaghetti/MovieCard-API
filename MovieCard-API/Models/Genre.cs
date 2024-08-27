using System.ComponentModel.DataAnnotations;

namespace MovieCard_API.Models;

public class Genre
{
    public int Id { get; set; }
    [StringLength(30)] public string Name { get; set; }
    public ICollection<Movie> Movies { get; set; }
}