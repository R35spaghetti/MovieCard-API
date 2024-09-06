namespace MovieCard_API.DTOs;

public record DirectorDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }

}