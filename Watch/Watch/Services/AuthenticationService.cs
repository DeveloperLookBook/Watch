using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Watch.Extensions;
using Watch.Models.Users;
using Watch.Services.Repositories;
using Watch.Views;

namespace Watch.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        INavigationService NavigationService { get; }
        IUsersService      UserRepository    { get; }
        IUserService       UserService       { get; }

        public AuthenticationService(
            INavigationService navigationService, 
            IUsersService      userRepository,
            IUserService       userService)
        {
            this.NavigationService = navigationService;
            this.UserRepository    = userRepository;
            this.UserService       = userService;
        }

        public async Task<bool> SigninAsync(string login, string password)
        {
            bool areCredentialsValid = false;
            IUser user               = await this.UserRepository
                                                 .ReadAsync(q => q.FindByLogin(login));

            // If User login and password is valid, then redirect to MainPage.
            if (user != null && user.Credentials.Password == password)
            {
                this.UserService.Id = user.Id;

                await this.NavigationService.NavigateAsync($@"/{nameof(WatchListPage)}");

                areCredentialsValid = true;
            }

            return areCredentialsValid;
        }

        public void Signout()
        {
            this.NavigationService.NavigateAsync($@"/{nameof(SigninPage)}");
        }
    }
}
