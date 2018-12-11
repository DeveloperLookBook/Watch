using System.Threading.Tasks;

namespace Watch.Services
{
    public interface IAuthenticationService
    {
        Task<bool> SigninAsync(string login, string password);
        void       Signout();
    }
}