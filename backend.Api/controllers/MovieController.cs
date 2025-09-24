using backend.Api.models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _movieService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var movie = await _movieService.GetByIdAsync(id);
        if (movie == null) return NotFound();
        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Movie movie)
    {
        var newMovie = await _movieService.AddAsync(movie);
        return CreatedAtAction(nameof(GetById), new { id = newMovie.Id }, newMovie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Movie movie)
    {
        if (id != movie.Id) return BadRequest();
        var updatedMovie = await _movieService.UpdateAsync(movie);
        return Ok(updatedMovie);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _movieService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
