using MovieCard_API.Models;

namespace MovieCard_API.DTOs;

public record MovieDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Rating { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; }
 
}