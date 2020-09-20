namespace Finapp.Authentication.Exception
{
    public class UserNotFoundException : AuthenticationException
    {
        public string UserName { get; set; }
    }
}