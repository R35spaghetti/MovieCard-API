namespace MovieCard_API.Features;

public class MovieSortParameters
{
    public bool SortByTitle { get; set; }
    public SortDirection TitleOrder { get; set; }
    public bool SortByReleaseDate { get; set; }
    public SortDirection ReleaseDateOrder { get; set; }
    public bool SortByRating { get; set; }
    public SortDirection RatingOrder { get; set; }
}
public enum SortDirection
{
    Ascending,
    Descending
}
