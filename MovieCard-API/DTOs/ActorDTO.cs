namespace MovieCard_API.DTOs;

public record ActorDTO(
    string Name,
    DateTime Birthday,
    ICollection<MovieDTO> Movies
    );