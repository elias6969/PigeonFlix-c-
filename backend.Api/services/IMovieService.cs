using backend.Api.models;

public interface IMovieService
{
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(int id);
    Task<Movie> AddAsync(Movie movie);
    Task<Movie> UpdateAsync(Movie movie);
    Task<bool> DeleteAsync(int id);
}
