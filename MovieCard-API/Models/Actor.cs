using System.ComponentModel.DataAnnotations;

namespace MovieCard_API.Models;

public class Actor
{
    public int Id { get; set; }
    [StringLength(30)] public string Name { get; set; }
    [DataType(DataType.Date)] public DateTime Birthday { get; set; }
    public ICollection<Movie>? Movies { get; set; }
}