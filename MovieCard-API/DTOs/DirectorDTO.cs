using System.Collections;

namespace MovieCard_API.DTOs;

public record DirectorDTO(
    ContactInformationDTO ContactInformation,
    string Name,
    DateTime Birthday,
    ICollection<MovieDTO> Movies
    );