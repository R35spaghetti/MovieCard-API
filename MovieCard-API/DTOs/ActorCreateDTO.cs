using System.ComponentModel.DataAnnotations;

namespace MovieCard_API.DTOs;

public record ActorCreateDTO
{
    [StringLength(30)] public string Name { get; set; }
    [DataType(DataType.Date)] public DateTime Birthday { get; set; }
}