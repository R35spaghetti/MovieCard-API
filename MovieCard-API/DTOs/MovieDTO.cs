using MovieCard_API.Models;

namespace MovieCard_API.DTOs;

public record MovieDTO(
    int Id,
    Director? Director,
    ICollection<ActorDTO>? Actors,
    ICollection<GenreDTO>? Genres,
    string Title,
    string Rating,
    DateTime ReleaseDate,
    string Description
    );