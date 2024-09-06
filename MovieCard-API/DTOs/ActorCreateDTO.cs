namespace MovieCard_API.DTOs;

public record ActorCreateDTO
{
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public ICollection<MovieCreateDTO>? Movies { get; set; }

}