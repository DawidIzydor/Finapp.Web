using Newtonsoft.Json;

namespace Finapp.Dto.Authentication
{
    /// <summary>
    ///     Authentication response
    /// </summary>
    [JsonObject("AuthResponse")]
    public class AuthenticationResponseDto
    {
        /// <summary>
        ///     Authenticated user name
        /// </summary>
        [JsonRequired]
        public string Username { get; set; }
        /// <summary>
        ///     Token for user
        /// </summary>
        [JsonRequired]
        public string Token { get; set; }
    }
}