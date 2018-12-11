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

        public DelegateCommand MoveToCreateWatchPage { get; }
        public DelegateCommand RemoveWatch           { get; }
        public DelegateCommand MoveToUpdateWatchPage { get; }
        public DelegateCommand LoadWatches           { get; }
        #endregion


        #region CONSTRUCTORS

        public WatchListPageViewModel(
            INavigationService navigationService, 
            IUserService       userService,
            IUsersService      usersService,
            IWatchesService    watchesService)
            : base(navigationService)
        {
            this.User        = userService;
            this.Users       = usersService;
            this.Watches     = watchesService;

            this.MoveToCreateWatchPage = new DelegateCommand(this.MoveToCreateWatchPageAsync);
            this.RemoveWatch           = new DelegateCommand(this.RemoveWatchAsync          );
            this.MoveToUpdateWatchPage = new DelegateCommand(this.UpdateWatchAsync          );
            this.LoadWatches           = new DelegateCommand(this.LoadWatchesAsync          );
        }        

        #endregion


        #region METHODS

        private async void MoveToCreateWatchPageAsync()
        {
            await this.NavigationService.NavigateAsync($@"/{nameof(CreateWatchPage)}");
        }

        private async void RemoveWatchAsync()
        {
            var user  = await this.Users  .ReadAsync(q => q.FindById(this.User.Id        ));
            var watch = await this.Watches.ReadAsync(q => q.FindById(this.SelectedWatchId));

            user.Watches.Remove(watch as Models.Watches.Watch);

            this.Users.SaveChangesAsync();
        }

        private async void UpdateWatchAsync()
        {
            await this.NavigationService.NavigateAsync($@"/{nameof(UpdateWatchPage)}");
        }

        private async void LoadWatchesAsync()
        {
            this.UserWatches = await this.Users.ReadAsync(q => q.GetUserWatches(this.User.Id));
        }

        #endregion
    }
}
