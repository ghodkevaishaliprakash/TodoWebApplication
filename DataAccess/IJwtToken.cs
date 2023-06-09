using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public interface IJwtToken
    {

        Task<Token> GetAuthToken();

    }
}
