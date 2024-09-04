using System.Text.Json;
using System.Text.Json.Serialization;
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
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies([FromQuery] MovieSearchParameters parameters)
    {
        try
        {
            IEnumerable<MovieDTO> query = await _movieRepository.GetAllMoviesAsync();
            
            //ugly for now
            if (!string.IsNullOrEmpty(parameters.Title))
            {
                query = query.Where(m => m.Title.Contains(parameters.Title, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(parameters.Genre))
            {
                query = query.Where(m => m.Genres.Any(g => g.Name.Contains(parameters.Genre, StringComparison.OrdinalIgnoreCase)));
            }

            if (!string.IsNullOrEmpty(parameters.ActorName))
            {
                query = query.Where(m => m.Actors.Any(a => a.Name.Contains(parameters.ActorName, StringComparison.OrdinalIgnoreCase)));
            }

            if (!string.IsNullOrEmpty(parameters.DirectorName))
            {
                query = query.Where(m => m.Director.Name.Contains(parameters.DirectorName, StringComparison.OrdinalIgnoreCase));
            }

            if (parameters.ReleaseDateFrom.HasValue || parameters.ReleaseDateTo.HasValue)
            {
                query = query.Where(m =>
                    (!parameters.ReleaseDateFrom.HasValue || m.ReleaseDate >= parameters.ReleaseDateFrom.Value) &&
                    (!parameters.ReleaseDateTo.HasValue || m.ReleaseDate <= parameters.ReleaseDateTo.Value));
            }

            var movies =  query.ToList();

            if (!movies.Any())
            {
                throw new InvalidOperationException($"No movies found matching the criteria");
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var jsonString = JsonSerializer.Serialize(movies, options);
            return Ok(jsonString);
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

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var jsonString = JsonSerializer.Serialize(movie, options);
            return Ok(jsonString);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<MovieDTO>> CreateMovie([FromQuery] MovieDTO movie)
    {
        try
        {
            var createdMovie = await _movieRepository.CreateMovieAsync(movie);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var jsonString = JsonSerializer.Serialize(createdMovie, options);
            return Ok(jsonString);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<MovieDTO>> UpdateMovie([FromQuery] MovieDTO movie, int id)
    {
        try
        {
            var updateMovie = await _movieRepository.UpdateMovieAsync(movie, id);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var jsonString = JsonSerializer.Serialize(updateMovie, options);
            return Ok(jsonString);
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
        if (movie == null)
        {
            return NotFound("Movie not found");
        }

        try
        {
            patchMovie.ApplyTo(movie);
            TryValidateModel(movie);
            if (!ModelState.IsValid)
            {
                UnprocessableEntity(ModelState);
            }

            await _movieRepository.UpdateMovieAsync(movie, id);
            return Ok(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}