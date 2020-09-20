using System.Threading.Tasks;
using Finapp.Database;
using Finapp.Dto.Authentication;

namespace Finapp.Authentication
{
    public interface IUserService
    {
        Task<AuthenticationResponseDto> AuthenticateAsync(AuthenticationRequestDto requestModel);
        Task<FinappUser> GetByNameAsync(string name);
    }
}