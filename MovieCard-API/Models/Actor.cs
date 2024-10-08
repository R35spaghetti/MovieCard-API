﻿namespace MovieCard_API.Models;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}