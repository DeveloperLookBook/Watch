using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UpdateWatchPageViewModel : ViewModelBase
    {
        #region FIELDS

        private IWatch       _selectedUserWatch;
        private TimeZoneInfo _watchTimeZone;
        private Color        _watchDialColor;
        private Color        _watchArrowsColor;

        #endregion


        #region PROPERTIES

        public IWatch      SelectedUserWatch
        {
            get => this._selectedUserWatch;
            set => this.SetProperty(ref this._selectedUserWatch, value);
        }

        public TimeZoneInfo WatchTimeZone
        {
            get => this._watchTimeZone;
            set => this.SetProperty(ref this._watchTimeZone, value);
        }

        public Color        WatchDialColor
        {
            get => this._watchDialColor;
            set => this.SetProperty(ref this._watchDialColor, value);
        }

        public Color        WatchArrowsColor
        {
            get => this._watchArrowsColor;
            set => this.SetProperty(ref this._watchArrowsColor, value);
        }

        #endregion


        #region SERVICES

        private IUserService    UserService    { get; }
        private IUsersService   UsersService   { get; }
        private IWatchesService WatchesService { get; }

        #endregion


        #region COMMANDS

        public DelegateCommand EditWatchDialColor   => new DelegateCommand(async () =>
        {
            await this.NavigateToDialColoListPageAsync();
        });
        public DelegateCommand EditWatchArrowsColor => new DelegateCommand(async () =>
        {
            await this.NavigateToArrowsColorListPageAsync();
        });
        public DelegateCommand EditTimeZoneInfo     => new DelegateCommand(async () =>
        {
            await this.NavigateToTimeZoneListPageAsync();
        });
        public DelegateCommand DeleteWatch          => new DelegateCommand(async () =>
        {
            await this.DeleteWatchAsync();
            await this.NavigateToWatchListPageAsync();
        });
        public DelegateCommand SaveWatch           => new DelegateCommand(async () =>
        {
            await this.SaveWatchAsync();
            await this.NavigateToWatchListPageAsync();
        });
        public DelegateCommand CanselWatchChanges  => new DelegateCommand(async () =>
        {
            await this.NavigateToWatchListPageAsync();
        });

        #endregion


        #region CONSTRUCTORS

        public UpdateWatchPageViewModel(
            INavigationService navigationService,
            IUserService       userService,
            IUsersService      usersService,
            IWatchesService    watchesService) 
            : base(navigationService)
        {
            // Initialize services:
            this.UserService    = userService;
            this.UsersService   = usersService;
            this.WatchesService = watchesService;

            // Set default watch settings:
            this.WatchTimeZone    = TimeZoneInfo.Local;
            this.WatchDialColor   = Color.LightGray;
            this.WatchArrowsColor = Color.LightPink;
        }        

        #endregion


        #region METHODS

        private async Task NavigateToDialColoListPageAsync   ()
        {
            var parameters = new NavigationParameters
            {
                { nameof(this.WatchDialColor), this.WatchDialColor }
            };

            await this.NavigationService.NavigateAsync($@"{nameof(ColorListPage)}", parameters);
        }
        private async Task NavigateToArrowsColorListPageAsync()
        {
            var parameters = new NavigationParameters
            {
                { nameof(this.WatchArrowsColor), this.WatchArrowsColor }
            };

            await this.NavigationService.NavigateAsync($@"{nameof(ColorListPage)}", parameters);
        }
        private async Task NavigateToTimeZoneListPageAsync   ()
        {
            var parameters = new NavigationParameters()
            {
                { nameof(this.WatchTimeZone), this.WatchTimeZone }
            };

            await this.NavigationService.NavigateAsync($@"{nameof(TimeZoneListPage)}", parameters);
        }
        private async Task NavigateToWatchListPageAsync      ()
        {
            await this.NavigationService.GoBackAsync();
        }

        private async Task DeleteWatchAsync()
        {
            var user = await this.UsersService.ReadAsync(q => q.FindById(this.UserService.Id));

            user.Watches.Remove(this.SelectedUserWatch as Models.Watches.Watch);

            this.UsersService.SaveChangesAsync();
        }
        private async Task SaveWatchAsync  ()
        {
            IUser user = await this.UsersService.ReadAsync(q => q.FindById(this.UserService.Id));

            this.SelectedUserWatch.TimeZone    = this.WatchTimeZone;
            this.SelectedUserWatch.DialColor   = this.WatchDialColor;
            this.SelectedUserWatch.ArrowsColor = this.WatchArrowsColor;

            await this.WatchesService.UpdateAsync(this.SelectedUserWatch as Models.Watches.Watch);
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(this.WatchDialColor)))
            {
                this.WatchDialColor = parameters.GetValue<Color>(nameof(this.WatchDialColor));
            }
            else if (parameters.ContainsKey(nameof(this.WatchArrowsColor)))
            {
                this.WatchArrowsColor = parameters.GetValue<Color>(nameof(this.WatchArrowsColor));
            }
            else if (parameters.ContainsKey(nameof(this.WatchTimeZone)))
            {
                this.WatchTimeZone = parameters.GetValue<TimeZoneInfo>(nameof(this.WatchTimeZone));
            }
            else if (parameters.ContainsKey("SelectedUserWatch"))
            {
                this.SelectedUserWatch = parameters.GetValue<IWatch>("SelectedUserWatch");

                this.WatchTimeZone    = this.SelectedUserWatch.TimeZone;
                this.WatchDialColor   = this.SelectedUserWatch.DialColor;
                this.WatchArrowsColor = this.SelectedUserWatch.ArrowsColor;
            }
        }

        #endregion
    }
}
