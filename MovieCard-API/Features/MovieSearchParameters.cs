namespace MovieCard_API.Features;

public  class MovieSearchParameters
{
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public string? ActorName { get; set; }
    public string? DirectorName { get; set; }
    public DateTime? ReleaseDateFrom { get; set; }
    public DateTime? ReleaseDateTo { get; set; }
    public bool IncludeActors { get; set; } = false;
}
