
namespace MovieCard_API.Models;

public class Director
{
    public int Id { get; set; }
    public int ContactInformationId { get; set; }
    public ContactInformation? ContactInformation { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public ICollection<Movie> Movies { get; set; }

}