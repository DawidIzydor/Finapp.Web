using Newtonsoft.Json;

namespace Finapp.Dto.Authentication
{
    /// <summary>
    ///     Authentication request
    /// </summary>
    [JsonObject("AuthRequest")]
    public class AuthenticationRequestDto
    {
        /// <summary>
        ///     Username
        /// </summary>
        [JsonRequired]
        public string Username { get; set; }
        /// <summary>
        ///     Password
        /// </summary>
        [JsonRequired]
        public string Password { get; set; }
    }
}