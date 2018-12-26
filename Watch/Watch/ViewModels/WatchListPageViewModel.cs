using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watch.Extensions;
using Watch.Models.Users;
using Watch.Models.Watches;
using Watch.Services;
using Watch.Services.Repositories;
using Watch.Views;
using Xamarin.Forms;

namespace Watch.ViewModels
{
    public class WatchListPageViewModel : ViewModelBase
    {
        #region FIELDS

        private IWatch       _selectedUserWatch;
        private List<IWatch> _watches;

        #endregion


        #region PROPERTIES

        public IWatch      SelectedUserWatch
        {
            get => this._selectedUserWatch;
            set
            {
                this.SetProperty(ref this._selectedUserWatch, value);

                if (value != null) { this.UpdateWatch.Execute(); }
            }
        }

        public List<IWatch> UserWatches
        {
            get => this._watches;
            set => this.SetProperty(ref this._watches, value);
        }

        #endregion


        #region SERVICES

        private IUserService    UserService    { get; }
        private IUsersService   UsersService   { get; }
        private IWatchesService WatchesService { get; }

        #endregion


        #region COMMANDS

        public DelegateCommand CreateWatch     => new DelegateCommand(async () =>
        {
            await this.NavigateToCreateWatchPageAsync();
        });
        public DelegateCommand UpdateWatch     => new DelegateCommand(async () =>
        {
            await this.NavigateToUpdateWatchPageAsync();
        });
        public DelegateCommand UpdateWatchList => new DelegateCommand(async () =>
        {
            await this.UpdateUserWatchListAsync();
        });

        #endregion


        #region CONSTRUCTORS

        public WatchListPageViewModel(
            INavigationService navigationService, 
            IUserService       userService,
            IUsersService      usersService,
            IWatchesService    watchesService)
            : base(navigationService)
        {
            this.UserService    = userService;
            this.UsersService   = usersService;
            this.WatchesService = watchesService;

            this.InitializeWatchListAsync();
        }        

        #endregion


        #region METHODS

        private async Task NavigateToCreateWatchPageAsync()
        {
            await this.NavigationService.NavigateAsync($@"{nameof(CreateWatchPage)}");
        }
        private async Task NavigateToUpdateWatchPageAsync()
        {
            var parameters = new NavigationParameters()
            {
                { nameof(this.SelectedUserWatch) , this.SelectedUserWatch }
            };
            await this.NavigationService.NavigateAsync($@"{nameof(UpdateWatchPage)}", parameters);
        }

        private async Task UpdateUserWatchListAsync()
        {
            this.UserWatches = await this.UsersService.ReadAsync(
                q => q.GetUserWatches(this.UserService.Id));
        }
        private async void InitializeWatchListAsync()
        {
            this.UserWatches = await this.UsersService.ReadAsync(
                q => q.GetUserWatches(this.UserService.Id));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            this.SelectedUserWatch = null;
            this.UpdateWatchList.Execute();
        }

        #endregion
    }
}
