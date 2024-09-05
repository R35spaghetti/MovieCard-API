namespace MovieCard_API.DTOs;

public record DirectorCreateDTO(
    string Name,
    DateTime Birthday,
    ContactInformationCreateDTO ContactInformation,
    ICollection<MovieCreateDTO>? Movies

);