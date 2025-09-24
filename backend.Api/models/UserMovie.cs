using Microsoft.AspNetCore.Identity;

namespace backend.Api.models
{
    public class UserMovie
    {
        public string UserId { get; set; } = string.Empty;
        public IdentityUser User { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public bool IsFavorite { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
