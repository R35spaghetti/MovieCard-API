namespace MovieCard_API.DTOs;

public record GenreCreateDTO
{
   public string Name { get; set; }
   public ICollection<MovieCreateDTO>? Movies { get; set; }
}