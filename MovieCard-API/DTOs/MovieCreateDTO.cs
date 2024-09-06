namespace MovieCard_API.DTOs;

public record MovieCreateDTO
{
    public string Title { get; set; }
    public string Rating { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; }
    public DirectorCreateDTO? Director { get; set; }
    public ICollection<ActorCreateDTO>? Actors { get; set; }
    public ICollection<GenreDTO>? Genres { get; set; }
}