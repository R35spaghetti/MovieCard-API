using AutoMapper;
using MovieCard_API.Data;
using MovieCard_API.Repositories;
using MovieCard_API.Repositories.contracts;

namespace MovieCard_API.Features.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieCardContext _dbContext;
    private readonly Lazy<IMovieRepository> _moviesRepository;

    public UnitOfWork(MovieCardContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _moviesRepository = new Lazy<IMovieRepository>(() => new MovieRepository(dbContext, mapper));
    }

    public IMovieRepository Movies => _moviesRepository.Value;

    public async Task CompleteAsync()
    {
        await _dbContext.SaveChangesAsync();
    }


    public void Dispose()
    {
        _dbContext.Dispose();
    }
}