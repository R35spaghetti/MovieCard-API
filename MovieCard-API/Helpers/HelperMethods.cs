using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Helpers;

public static class HelperMethods
{
    public static List<ActorCreateDTO> CheckDuplicateActors(IEnumerable<ActorCreateDTO> actorsToBeAdded,
        IEnumerable<Actor> currentActorsInTheMovie)
    {
        var duplicates = actorsToBeAdded.Where(dto =>
                currentActorsInTheMovie.Any(actor => actor.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase)))
            .ToList();


        return duplicates;
    }
}