using System.Collections;

namespace MovieCard_API.DTOs;

public record DirectorDTO(
    int Id,
    int ContactInformationId,
    ContactInformationDTO ContactInformation,
    string Name,
    DateTime Birthday,
    ICollection<MovieDTO> Movies
    );