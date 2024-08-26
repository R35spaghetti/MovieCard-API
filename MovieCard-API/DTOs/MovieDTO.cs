using MovieCard_API.Models;

namespace MovieCard_API.DTOs;

public record MovieDTO(
    int Id,
    int? DirectorId,
    Director? Director,
    ICollection<Actor>? Actors,
    ICollection<Genre>? Genres,
    string Title,
    string Rating,
    DateTime ReleaseDate,
    string Description
    );