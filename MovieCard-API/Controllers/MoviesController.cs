using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieCard_API.DTOs;
using MovieCard_API.Features;
using MovieCard_API.Features.UnitOfWork;

namespace MovieCard_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    
    public MoviesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies([FromQuery] MovieSearchParameters parameters,
        [FromQuery] MovieSortParameters sortBy)
    {
        try
        {
            var movies = await _unitOfWork.Movies.GetAllMoviesAsync();

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
            var movie = await _unitOfWork.Movies.GetMovieByIdAsync(id);
            
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
            var createdMovie = await _unitOfWork.Movies.CreateMovieAsync(movie);
            await _unitOfWork.CompleteAsync();

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
            var updateMovie = await _unitOfWork.Movies.UpdateMovieAsync(movie);
            await _unitOfWork.CompleteAsync();
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
            await _unitOfWork.Movies.DeleteMovieAsync(id);
            await _unitOfWork.CompleteAsync();
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
        var movie = await _unitOfWork.Movies.GetMovieByIdAsync(id);

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

            await _unitOfWork.Movies.UpdateMovieAsync(movieDto);
            await _unitOfWork.CompleteAsync();
            return Ok(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpPost("{id:int}")]
    public async Task<ActionResult<ActorCreateDTO>> AddActorsToMovie(int id, [FromQuery] IEnumerable<ActorCreateDTO> actors)
    {

        try
        {
            await _unitOfWork.Actors.AddActorToMovieAsync(id, actors);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}