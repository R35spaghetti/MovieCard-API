namespace MovieCard_API.DTOs;

public record DirectorCreateDTO
{
   public string Name { get; set; }
   public int? ContactInformationId { get; set; }
   public ContactInformationCreateDTO? ContactInformation { get; set; }
    public  DateTime Birthday { get; set; }
    public ICollection<MovieCreateDTO>? Movies { get; set; }

}