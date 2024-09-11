using AutoMapper;
using MovieCard_API.Data;
using MovieCard_API.Repositories;
using MovieCard_API.Repositories.contracts;

namespace MovieCard_API.Features.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieCardContext _dbContext;
    private readonly Lazy<IMovieRepository> _moviesRepository;
    private readonly Lazy<IActorRepository> _actorsRepository;

    public UnitOfWork(MovieCardContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext; 
        _moviesRepository = new Lazy<IMovieRepository>(() => new MovieRepository(dbContext, mapper));
        _actorsRepository = new Lazy<IActorRepository>(() => new ActorRepository(dbContext, mapper));
    }

    public IMovieRepository Movies => _moviesRepository.Value;
    
    public IActorRepository Actors => _actorsRepository.Value;

    public async Task CompleteAsync()
    {
        await _dbContext.SaveChangesAsync();
    }


    public void Dispose()
    {
        _dbContext.Dispose();
    }
}