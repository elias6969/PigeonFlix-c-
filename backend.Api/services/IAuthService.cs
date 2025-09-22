using backend.Api.models;

namespace backend.Api.services
{
    public interface IAuthService
    {
        Task<UserDto?> RegisterAsync(RegisterRequest request);
        Task<(string token, UserDto? user)?> LoginAsync(LoginRequest request);
    }
}

