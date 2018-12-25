using System.Threading.Tasks;

namespace Watch.Services
{
    public interface IAuthenticationService
    {
        Task<bool> IsUserValid(string login, string password);
    }
}