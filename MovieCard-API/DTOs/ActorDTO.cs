namespace MovieCard_API.DTOs;

public record ActorDTO(
    int Id,
    string Name,
    DateTime Birthday,
    ICollection<MovieDTO> Movies
    );