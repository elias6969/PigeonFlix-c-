using backend.Api.data;
using backend.Api.models;
using Microsoft.EntityFrameworkCore;

public class MovieService : IMovieService
{
    private readonly ApplicationDbContext _context;

    public MovieService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync() =>
        await _context.Movies.ToListAsync();

    public async Task<Movie?> GetByIdAsync(int id) =>
        await _context.Movies.FindAsync(id);

    public async Task<Movie> AddAsync(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie> UpdateAsync(Movie movie)
    {
        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return false;

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return true;
    }
}
