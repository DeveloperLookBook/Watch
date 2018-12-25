using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watch.Models.Users;
using Watch.Services;
using Watch.Views;
using Xamarin.Forms;

namespace Watch.ViewModels
{
    public class SigninPageViewModel : ViewModelBase
    {
        #region FIELDS

        private string _userLogin;
        private string _userPassword;
        private string _signinErrorMessage;

        #endregion

        #region NOTIFICATION PROPERTIES

        public string UserLogin
        {
            get => this._userLogin;
            set => this.SetProperty(ref this._userLogin, value);
        }

        public string UserPassword
        {
            get => this._userPassword;
            set => this.SetProperty(ref this._userPassword, value);
        }

        public string SigninErrorMessage
        {
            get => this._signinErrorMessage;
            set => this.SetProperty(ref this._signinErrorMessage, value);
        }

        #endregion


        #region SERVICES

        IAuthenticationService AuthenticationService { get; }

        #endregion


        #region COMMANDS

        public DelegateCommand Signin =>  new DelegateCommand(async () =>
        {
            var isUserValid = await this.IsUserValidAsync();

            if  (isUserValid) { this.NavigateToWatchListPageAsync(); }
            else              { this.ShowSiginErrorMessageToUser (); }
        });

        #endregion


        #region CONSTRUCTORS

        public SigninPageViewModel(
            INavigationService     navigationService, 
            IAuthenticationService authenticationService)
            : base(navigationService)
        {
            if (authenticationService is null)
            {
                throw new ArgumentNullException(nameof(authenticationService));
            }

            this.AuthenticationService = authenticationService ;
            this.Title                 = "Signin";
        }

        #endregion

        #region METHODS

        private async Task<bool> IsUserValidAsync            ()
        {
            return await this.AuthenticationService.IsUserValid(this.UserLogin, this.UserPassword);
        }
        private async void       NavigateToWatchListPageAsync()
        {
            await this.NavigationService.NavigateAsync($@"{nameof(WatchListPage)}");
        }
        private       void       ShowSiginErrorMessageToUser ()
        {
            this.SigninErrorMessage = "User login or password is invalid.";
        }

        #endregion
    }
}
