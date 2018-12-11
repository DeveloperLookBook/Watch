using Watch.ViewModels;
using Xamarin.Forms;


namespace Watch.Views
{
    public partial class WatchListPage : ContentPage
    {
        public WatchListPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var context = (WatchListPageViewModel)this.BindingContext;

            context?.LoadWatches.Execute();
        }
    }
}