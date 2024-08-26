using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using MovieCard_API.DTOs;

namespace MovieCard_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieRepository.MovieRepository _movieRepository;

    public MoviesController(MovieRepository.MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
    {
        var movies = await _movieRepository.GetAllMoviesAsync();

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        var jsonString = JsonSerializer.Serialize(movies, options);
        return Ok(jsonString);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies(int id)
    {
        var movie = await _movieRepository.GetMovieByIdAsync(id);

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        var jsonString = JsonSerializer.Serialize(movie, options);
        return Ok(jsonString);
    }

    [HttpPost]
    public async Task<ActionResult<MovieDTO>> CreateMovie([FromQuery] MovieDTO movie)
    {
        var createdMovie = await _movieRepository.CreateMovieAsync(movie);
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        var jsonString = JsonSerializer.Serialize(createdMovie, options);
        return Ok(jsonString);
    }

    [HttpPut]
    public async Task<ActionResult<MovieDTO>> UpdateMovie([FromQuery] MovieDTO movie, int id)
    {
        var updateMovie = await _movieRepository.UpdateMovieAsync(movie, id);

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        var jsonString = JsonSerializer.Serialize(updateMovie, options);
        return Ok(jsonString);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteMovie(int id)
    {
        _movieRepository.DeleteMovieAsync(id);
        return Ok("Deleted");
    }
}