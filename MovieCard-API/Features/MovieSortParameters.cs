namespace MovieCard_API.Features;

public class MovieSortParameters
{
    public bool SortByTitle { get; set; }
    public bool TitleOrder { get; set; }
    public bool SortByReleaseDate { get; set; }
    public bool ReleaseDateOrder { get; set; }
    public bool SortByRating { get; set; }
    public bool RatingOrder { get; set; }
}

