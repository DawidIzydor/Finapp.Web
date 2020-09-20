using Newtonsoft.Json;

namespace Finapp.Dto.Authentication
{
    [JsonObject("AuthRequest")]
    public class AuthenticationRequestDto
    {
        [JsonRequired]
        public string Username { get; set; }
        [JsonRequired]
        public string Password { get; set; }
    }
}