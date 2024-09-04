namespace MovieCard_API.Features;

public  class MovieSearchParameters
{
    public string? Title { get; }
    public string? Genre { get; }
    public string? ActorName { get; }
    public string? DirectorName { get; }
    public DateTime? ReleaseDateFrom { get; }
    public DateTime? ReleaseDateTo { get;  }
}
