using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watch.Extensions;
using Watch.Models.Users;
using Watch.Models.Watches;
using Watch.Services;
using Watch.Services.Repositories;
using Watch.Views;

namespace Watch.ViewModels
{
    public class WatchListPageViewModel : ViewModelBase
    {
        #region FIELDS

        private int          _selectedWatchId;
        private List<IWatch> _watches;

        #endregion

        #region PROPERTIES

        public int          SelectedWatchId
        {
            get => this._selectedWatchId;
            set => this.SetProperty(ref this._selectedWatchId, value);
        }

        public List<IWatch> UserWatches
        {
            get => this._watches;
            set => this.SetProperty(ref this._watches, value);
        }

        #endregion

        #region SERVICES

        private IUserService    User    { get; }
        private IUsersService   Users   { get; }
        private IWatchesService Watches { get; }

        #endregion


        #region COMMANDS

        public DelegateCommand CreateWatch => new DelegateCommand(() => 
        {
            this.NavigateToCreateWatchPageAsync();
        });
        public DelegateCommand RemoveWatch => new DelegateCommand(() =>
        {
            this.RemoveUserWatchAsync();
        });
        public DelegateCommand UpdateWatch => new DelegateCommand(() =>
        {
            this.NavigateToUpdateWatchPageAsync();
        });
        public DelegateCommand LoadWatches => new DelegateCommand(() =>
        {
            this.LoadWatchesAsync();
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
            this.User    = userService;
            this.Users   = usersService;
            this.Watches = watchesService;
        }        

        #endregion


        #region METHODS

        private async void NavigateToCreateWatchPageAsync()
        {
            await this.NavigationService.NavigateAsync($@"{nameof(CreateWatchPage)}");
        }
        private async void NavigateToUpdateWatchPageAsync()
        {
            await this.NavigationService.NavigateAsync($@"{nameof(UpdateWatchPage)}");
        }

        private async void RemoveUserWatchAsync()
        {
            var user  = await this.Users  .ReadAsync(q => q.FindById(this.User.Id        ));
            var watch = await this.Watches.ReadAsync(q => q.FindById(this.SelectedWatchId));

            user.Watches.Remove(watch as Models.Watches.Watch);

            this.Users.SaveChangesAsync();
        }
        private async void LoadWatchesAsync    ()
        {
            this.UserWatches = await this.Users.ReadAsync(q => q.GetUserWatches(this.User.Id));
        }

        #endregion
    }
}
