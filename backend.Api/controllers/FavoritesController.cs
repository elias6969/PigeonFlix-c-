using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace backend.Api.controllers
{
    [ApiController]
    [Route("api/favorites")]
    [Authorize] // JWT protection
    public class FavoritesController : ControllerBase
    {
        private readonly IUserMovieService _userMovieService;

        public FavoritesController(IUserMovieService userMovieService)
        {
            _userMovieService = userMovieService;
        }

        [HttpPost("{movieId}")]
        public async Task<IActionResult> AddFavorite(int movieId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var favorite = await _userMovieService.AddFavoriteAsync(userId, movieId);

            Console.WriteLine($"[DEBUG] FavoritesController hit with movieId={movieId}");

            return Ok(new { userId, movieId, favorite });
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> RemoveFavorite(int movieId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var removed = await _userMovieService.RemoveFavoriteAsync(userId, movieId);
            return removed ? NoContent() : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var favorites = await _userMovieService.GetFavoritesAsync(userId);
            return Ok(favorites);
        }
    }
}

