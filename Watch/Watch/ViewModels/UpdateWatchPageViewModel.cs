using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Watch.ViewModels
{
    public class UpdateWatchPageViewModel : ViewModelBase
    {
        public UpdateWatchPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
        }
    }
}
