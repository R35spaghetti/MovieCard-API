using MovieCard_API.Models;

namespace MovieCard_API.DTOs;

public record ContactInformationDTO
(
    Director Director,
    string Email,
    int PhoneNumber
);