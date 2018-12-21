using DryIoc;
using Prism;
using Prism.Ioc;
using Watch.Databases;
using Watch.Extensions;
using Watch.Models.Users;
using Watch.Services;
using Watch.Services.Repositories;
using Watch.ViewModels;
using Watch.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Watch
{
    public partial class App
    {
        private IAppDbContext Context       { get; set; }

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            this.InitializeComponent();

            this.AddSeedToDataBase(Container.Resolve<IAppDbContext>());

            await NavigationService.NavigateAsync($@"NavigationPage/{nameof(SigninPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Injections:
            containerRegistry.RegisterSingleton<IAppDbContext          , AppDbContext         >();
            containerRegistry.RegisterSingleton<IUsersService          , UsersService         >();
            containerRegistry.RegisterSingleton<IWatchesService        , WatchesService       >();
            containerRegistry.RegisterSingleton<IAuthenticationService , AuthenticationService>();
            containerRegistry.RegisterSingleton<IUserService           , UserService          >();
            containerRegistry.RegisterSingleton<IColorService          , ColorService         >();

            // Navigation pages:
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<SigninPage      , SigninPageViewModel      >();
            containerRegistry.RegisterForNavigation<WatchListPage   , WatchListPageViewModel   >();
            containerRegistry.RegisterForNavigation<UpdateWatchPage , UpdateWatchPageViewModel >();
            containerRegistry.RegisterForNavigation<CreateWatchPage , CreateWatchPageViewModel >();
            containerRegistry.RegisterForNavigation<TimeZoneListPage, TimeZoneListPageViewModel>();
            containerRegistry.RegisterForNavigation<ColorListPage   , ColorListPageViewModel   >();
        }

        public void AddSeedToDataBase(IAppDbContext context)
        {
            if (!context.Users.ContainUserWithLogin("Admin"))
            {
                context.Add(new User
                {
                    Credentials = new Credentials()
                    {
                        Login    = "Admin",
                        Password = "Admin"
                    }
                });

                context.SaveChanges();
            }
        }

        ~App()
        {
            this.Context?.Dispose();
            this.Context = null;
        }
    }
}
