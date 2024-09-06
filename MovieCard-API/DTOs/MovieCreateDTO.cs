namespace MovieCard_API.DTOs;

public record MovieCreateDTO
{
    public string Title { get; set; }
    public int Rating { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; }
    public int DirectorId { get; set; }
    public int ActorIds { get; set; }
    public int GenreIds { get; set; }
}