using MovieCard_API.Models;

namespace MovieCard_API.DTOs;

public record ContactInformationDTO
(
    int Id,
    int DirectorId,
    Director Director,
    string Email,
    int PhoneNumber
);