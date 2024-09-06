namespace MovieCard_API.DTOs;

public record ContactInformationCreateDTO
{
    public int DirectorId { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
}