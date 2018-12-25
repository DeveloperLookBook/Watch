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
    public class CreateWatchPageViewModel : ViewModelBase
    {
        #region FIELDS

        private TimeZoneInfo _watchTimeZone;
        private Color        _watchDialColor;
        private Color        _watchArrowsColor;

        #endregion


        #region PROPERTIES

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

        private IUserService  User  { get; }
        private IUsersService Users { get; }

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
        public DelegateCommand SaveWatch            => new DelegateCommand(async () =>
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

        public CreateWatchPageViewModel(
            INavigationService navigationService,
            IUserService       userService,
            IUsersService      usersService) 
            : base(navigationService)
        {
            // Initialize services:
            this.User  = userService;
            this.Users = usersService;

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


        private async Task SaveWatchAsync()
        {
            IUser user = await this.Users.ReadAsync(q => q.FindById(this.User.Id));

            user.Watches.Add(new Models.Watches.Watch
            {
                TimeZone    = this.WatchTimeZone,
                ArrowsColor = this.WatchArrowsColor,
                DialColor   = this.WatchDialColor
            });

            this.Users.SaveChangesAsync();
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
        }

        #endregion
    }
}
