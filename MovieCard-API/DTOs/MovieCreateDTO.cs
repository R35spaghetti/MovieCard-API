namespace MovieCard_API.DTOs;

public record MovieCreateDTO
{
    public string Title { get; set; }
    public int Rating { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; }
    public int? DirectorId { get; set; }
    public IEnumerable<int>? ActorIds { get; set; }
    public IEnumerable<int>? GenreIds { get; set; }
}