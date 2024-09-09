using System.ComponentModel.DataAnnotations;
using MovieCard_API.Custom_attributes;

namespace MovieCard_API.DTOs;

public record MovieCreateDTO
{
    [StringLength(30)] [IsTitleUnique] public string Title { get; set; } = string.Empty;
    [Range(0, 10)] public int Rating { get; set; }

    [DataType(DataType.Date)]
    [IsDateSetInFuture]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime ReleaseDate { get; set; }

    [StringLength(100)] public string Description { get; set; }
    public int? DirectorId { get; set; }
    public IEnumerable<int>? ActorIds { get; set; }
    public IEnumerable<int>? GenreIds { get; set; }
}