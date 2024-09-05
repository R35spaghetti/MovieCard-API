namespace MovieCard_API.DTOs;

public record ContactInformationCreateDTO(
    string Email,
    DirectorCreateDTO Director,
    int PhoneNumber
);