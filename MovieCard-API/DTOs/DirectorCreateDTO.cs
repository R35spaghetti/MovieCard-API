using System.ComponentModel.DataAnnotations;

namespace MovieCard_API.DTOs;

public record DirectorCreateDTO
{
    [StringLength(30)] public string Name { get; set; }
    public int? ContactInformationId { get; set; }
    [DataType(DataType.Date)] public DateTime Birthday { get; set; }
}