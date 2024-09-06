namespace MovieCard_API.DTOs;

public record ActorDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public ICollection<MovieDTO>? Movies { get; set; }
}