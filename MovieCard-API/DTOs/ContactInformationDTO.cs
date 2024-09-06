namespace MovieCard_API.DTOs;

public record ContactInformationDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
}