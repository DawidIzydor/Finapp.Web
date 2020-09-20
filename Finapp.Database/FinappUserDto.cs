namespace Finapp.Database
{
    /// <summary>
    ///     User DTO for authorization
    /// </summary>
    public class FinappUserDto
    {
        /// <summary>
        ///     Username
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     Password hash
        /// </summary>
        public string PasswordHash { get; set; }
    }
}