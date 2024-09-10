using System.ComponentModel.DataAnnotations;

namespace MovieCard_API.DTOs;

public record GenreCreateDTO
{
    [StringLength(30)] public string Name { get; set; }
}