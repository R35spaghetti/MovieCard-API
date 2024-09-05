namespace MovieCard_API.DTOs;

public record DirectorDTO(
    int Id,
    ContactInformationDTO ContactInformation,
    string Name,
    DateTime Birthday,
    ICollection<MovieDTO> Movies
);