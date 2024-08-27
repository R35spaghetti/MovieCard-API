using System.ComponentModel.DataAnnotations;

namespace MovieCard_API.Models;

public class ContactInformation
{
    public int Id { get; set; }
    public int DirectorId { get; set; }
    public Director Director { get; set; }
    [StringLength(60)] public string Email { get; set; }
    [Phone] public int PhoneNumber { get; set; }
}