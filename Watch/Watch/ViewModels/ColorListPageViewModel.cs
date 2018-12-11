using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Watch.Services;
using Watch.Views;
using Xamarin.Forms;

namespace Watch.ViewModels
{
    public class ColorListPageViewModel : ViewModelBase
    {
        #region FIELDS

        public Color       _selectedColor;
        public List<Color> _colors;

        #endregion


        #region PROPERTIES

        public Color SelectedColor
        {
            get => this._selectedColor;
            set => this.SetProperty(ref this._selectedColor, value);
        }

        public List<Color> Colors
        {
            get => this._colors;
            set => this.SetProperty(ref this._colors, value);
        }

        public string NavigationParameterKey { get; set; }

        #endregion


        #region COMMANDS

        public DelegateCommand MoveToCreateWatchPage { get; }

        #endregion


        #region CONSTRUCTORS

        public ColorListPageViewModel(INavigationService navigationService, IColorService colorService) 
            : base(navigationService)
        {
            if (colorService is null) { throw new ArgumentNullException(nameof(colorService)); }

            this.Colors                = colorService.Colors;
            this.MoveToCreateWatchPage = new DelegateCommand(MoveToCreateWatchPageAsync);
        }

        #endregion


        #region METHODS

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo  (INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("WatchDialColor"))
            {
                this.NavigationParameterKey = "WatchDialColor";
                this.SelectedColor          = parameters.GetValue<Color>("WatchDialColor");
            }
            else if (parameters.ContainsKey("WatchArrowsColor"))
            {
                this.NavigationParameterKey = "WatchArrowsColor";
                this.SelectedColor          = parameters.GetValue<Color>("WatchArrowsColor");
            }
        }

        public async void MoveToCreateWatchPageAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { this.NavigationParameterKey, this.SelectedColor }
            };
            
            await this.NavigationService.NavigateAsync(nameof(CreateWatchPage), parameters);
            
            // This method doesn't work =(
            //await this.NavigationService.GoBackAsync(parameters);
        }

        #endregion
    }
}
