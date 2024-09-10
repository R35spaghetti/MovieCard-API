using System.ComponentModel.DataAnnotations;

namespace MovieCard_API.DTOs;

public record ContactInformationCreateDTO
{
    public int DirectorId { get; set; }
    [StringLength(60)] public string Email { get; set; }
    [Phone] public int PhoneNumber { get; set; }
}