using backend.Api.models;

public interface IUserMovieService
{
    Task<UserMovie> AddFavoriteAsync(string userId, int movieId);
    Task<bool> RemoveFavoriteAsync(string userId, int movieId);
    Task<IEnumerable<Movie>> GetFavoritesAsync(string userId);
}
