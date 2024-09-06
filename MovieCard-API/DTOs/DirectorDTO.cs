namespace MovieCard_API.DTOs;

public record DirectorDTO
{
    public int Id { get; set; }
    public int? ContactInformationId { get; set; }
    public ContactInformationDTO? ContactInformation { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public ICollection<MovieDTO>? Movies { get; set; }

}