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


        #region PROPERTIES

        public string UserLogin
        {
            get => this._userLogin;
            set => this.SetProperty(ref this._userLogin   , value, nameof(this.UserLogin));
        }

        public string UserPassword
        {
            get => this._userPassword;
            set => this.SetProperty(ref this._userPassword, value, nameof(this.UserPassword));
        }

        public string SigninErrorMessage
        {
            get => this._signinErrorMessage;
            set => this.SetProperty(ref this._signinErrorMessage, value, nameof(this.SigninErrorMessage));
        }

        #endregion


        #region SERVICES

        IAuthenticationService AuthenticationService { get; }
        IPageDialogService     PageDialogService     { get; }

        #endregion


        #region COMMANDS

        public DelegateCommand MoveToWatchListPage  { get; }

        #endregion


        #region CONSTRUCTORS

        public SigninPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
            : base(navigationService)
        {
            if (authenticationService is null) throw new ArgumentNullException(nameof(authenticationService));

            this.AuthenticationService = authenticationService ;
            this.MoveToWatchListPage   = new DelegateCommand(this.MoveToWatchListPageAsync);
            this.Title                 = "Signin";
        }

        #endregion

        #region METHODS

        private async void MoveToWatchListPageAsync()
        {
            if (!await this.AuthenticationService.SigninAsync(this.UserLogin, this.UserPassword))
            {
                this.SigninErrorMessage = "User login or password is invalid.";
            }
        }

        #endregion
    }
}
