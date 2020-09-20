using Newtonsoft.Json;

namespace Finapp.Dto.Authentication
{
    [JsonObject("AuthResponse")]
    public class AuthenticationResponseDto
    {
        [JsonRequired]
        public string Username { get; set; }
        [JsonRequired]
        public string Token { get; set; }
    }
}