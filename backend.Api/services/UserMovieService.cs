using backend.Api.data;
using backend.Api.models;
using Microsoft.EntityFrameworkCore;

public class UserMovieService : IUserMovieService
{
    private readonly ApplicationDbContext _context;

    public UserMovieService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<UserMovie> AddFavoriteAsync(string userId, int movieId)
    {
        var existing = await _context.UserMovies
            .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

        if (existing != null)
            return existing;

        var userMovie = new UserMovie
        {
            UserId = userId,
            MovieId = movieId,
            IsFavorite = true,
            AddedAt = DateTime.UtcNow
        };

        _context.UserMovies.Add(userMovie);
        await _context.SaveChangesAsync();

        // 🔎 Force reload to confirm insert
        return await _context.UserMovies
            .FirstAsync(um => um.UserId == userId && um.MovieId == movieId);
    }


    public async Task<bool> RemoveFavoriteAsync(string userId, int movieId)
    {
        var userMovie = await _context.UserMovies
            .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

        if (userMovie == null)
            return false;

        _context.UserMovies.Remove(userMovie);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Movie>> GetFavoritesAsync(string userId)
    {
        return await _context.UserMovies
            .Where(um => um.UserId == userId && um.IsFavorite)
            .Select(um => um.Movie)
            .ToListAsync();
    }
}
