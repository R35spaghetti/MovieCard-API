using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieCard_API.DTOs;
using MovieCard_API.Features;
using MovieCard_API.Repositories;

namespace MovieCard_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieRepository _movieRepository;

    public MoviesController(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies([FromQuery] MovieSearchParameters parameters,
        [FromQuery] MovieSortParameters sortBy)
    {
        try
        {
            var movies = await _movieRepository.GetAllMoviesAsync();

            movies = SortMovies.ApplyFilters(movies, parameters);

            var sortedMovies = SortMovies.GetSortedMovies(sortBy, movies);
            var result = sortedMovies.ConvertMovies();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies(int id)
    {
        try
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);

            return Ok(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<MovieCreateDTO>> CreateMovie([FromQuery] MovieCreateDTO movie)
    {
        try
        {
            var createdMovie = await _movieRepository.CreateMovieAsync(movie);

            return Ok(createdMovie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<MovieDTO>> UpdateMovie([FromQuery] MovieDTO movie)
    {
        try
        {
            var updateMovie = await _movieRepository.UpdateMovieAsync(movie);

            return Ok(updateMovie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteMovie(int id)
    {
        try
        {
            await _movieRepository.DeleteMovieAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> PatchMovie(JsonPatchDocument<MovieDTO> patchMovie, int id)
    {
        if (patchMovie is null) return BadRequest("No patch doc found");

        var movie = await _movieRepository.GetMovieByIdAsync(id);
        var movieDto = movie.ConvertOneMovie();
        if (movie == null)
        {
            return NotFound("Movie not found");
        }

        try
        {
            patchMovie.ApplyTo(movieDto);
            TryValidateModel(movie);
            if (!ModelState.IsValid)
            {
                UnprocessableEntity(ModelState);
            }

            await _movieRepository.UpdateMovieAsync(movieDto);
            return Ok(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}