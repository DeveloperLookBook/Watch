using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Watch.Views;

namespace Watch.ViewModels
{
	public class TimeZoneListPageViewModel : ViewModelBase
	{
        private List<TimeZoneInfo> _timeZones;
        private TimeZoneInfo       _selectedTimeZone;


        public List<TimeZoneInfo> TimeZones
        {
            get => this._timeZones;
            set => this.SetProperty(ref this._timeZones, value);
        }

        public TimeZoneInfo       SelectedTimeZone
        {
            get => this._selectedTimeZone;
            set => this.SetProperty(ref this._selectedTimeZone, value);
        }



        public DelegateCommand MoveToCreateWatchPage { get; }


        public TimeZoneListPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            this.TimeZones        = TimeZoneInfo.GetSystemTimeZones().ToList();
            this.SelectedTimeZone = TimeZoneInfo.Local;

            this.MoveToCreateWatchPage = new DelegateCommand(MoveToCreateWatchPageAsync);
        }


        public void MoveToCreateWatchPageAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { nameof( this.SelectedTimeZone), this.SelectedTimeZone }
            };

            this.NavigationService.NavigateAsync(nameof(CreateWatchPage), parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            this.SelectedTimeZone = parameters.GetValue<TimeZoneInfo>(nameof(this.SelectedTimeZone));
        }
    }
}
