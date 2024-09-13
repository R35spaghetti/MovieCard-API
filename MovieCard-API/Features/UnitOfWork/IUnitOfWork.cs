using MovieCard_API.Repositories.contracts;

namespace MovieCard_API.Features.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task CompleteAsync();

    IMovieRepository Movies { get; }
    
}
