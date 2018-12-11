using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public TimeZoneInfo _watchTimeZone;
        public Color        _watchDialColor;
        public Color        _watchArrowsColor;

        #endregion

        #region BINDABLE PROPERTIES

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

        public DelegateCommand SelectWatchDialColor   { get; }
        public DelegateCommand SelectWatchArrowsColor { get; }
        public DelegateCommand MoveToTimeZoneListPage { get; }
        public DelegateCommand SaveWatch              { get; }
        public DelegateCommand MoveToWatchListPage    { get; }

        #endregion


        #region CONSTRUCTORS

        public CreateWatchPageViewModel(
            INavigationService navigationService,
            IUserService       userService,
            IUsersService      usersService) 
            : base(navigationService)
        {
            // Init services:
            this.User  = userService;
            this.Users = usersService;

            // Create commands
            this.SelectWatchDialColor   = new DelegateCommand(this.SelectWatchDialColorAsync  );
            this.SelectWatchArrowsColor = new DelegateCommand(this.SelectWatchArrowsColorAsync);
            this.MoveToTimeZoneListPage = new DelegateCommand(this.MoveToTimeZoneListPageAsync);
            this.SaveWatch              = new DelegateCommand(this.SaveWatchAsync             );
            this.MoveToWatchListPage    = new DelegateCommand(this.MoveToWatchListPageAsync   );

            // Set default watch settings:
            this.WatchTimeZone    = TimeZoneInfo.Local;
            this.WatchDialColor   = Color.LightGray;
            this.WatchArrowsColor = Color.LightPink;
        }        

        #endregion


        #region METHODS

        private async void SelectWatchDialColorAsync()
        {
            var parameters = new NavigationParameters
            {
                { nameof(this.WatchDialColor), this.WatchDialColor }
            };

            await this.NavigationService.NavigateAsync($@"/{nameof(ColorListPage)}", parameters);
        }

        private async void SelectWatchArrowsColorAsync()
        {
            var parameters = new NavigationParameters
            {
                { nameof(this.WatchArrowsColor), this.WatchArrowsColor }
            };

            await this.NavigationService.NavigateAsync($@"/{nameof(ColorListPage)}", parameters);
        }

        private async void MoveToTimeZoneListPageAsync()
        {
            await this.NavigationService.NavigateAsync($@"/{nameof(TimeZoneListPage)}");
        }

        private async void SaveWatchAsync()
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

        private async void MoveToWatchListPageAsync()
        {
            await this.NavigationService.NavigateAsync($@"/{nameof(WatchListPage)}");
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
        }

        #endregion
    }
}
