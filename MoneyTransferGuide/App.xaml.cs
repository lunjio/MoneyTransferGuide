using MoneyTransferGuide.Helpers;
using Prism;
using Prism.Ioc;
using MoneyTransferGuide.ViewModels;
using MoneyTransferGuide.Views;
using MoneyTransferGuide.Services;
using MoneyTransferGuide.Themes;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MoneyTransferGuide
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null)
        {

        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
            Device.SetFlags(new string[] { "Expander_Experimental"});

            Application.Current.RequestedThemeChanged += (s, a) =>
            {
                if (a.RequestedTheme == OSAppTheme.Dark)
                {
                    PrismApplicationBase.Current.Resources.MergedDictionaries.Clear();
                    PrismApplicationBase.Current.Resources.MergedDictionaries.Add(new DarkTheme());
                    XF.Material.Forms.Material.Use("Material.DarkConfiguration"); 
                }
                else
                {
                    PrismApplicationBase.Current.Resources.MergedDictionaries.Clear();
                    PrismApplicationBase.Current.Resources.MergedDictionaries.Add(new LightTheme());
                    XF.Material.Forms.Material.Use("Material.LightConfiguration");
                }

            };
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            XF.Material.Forms.Material.Use("Material.LightConfiguration");
            await NavigationService.NavigateAsync("/CustomNavPage/MainPage");
        }

        protected override void OnStart()
        {
            Akavache.Registrations.Start(Container.Resolve<IAppInfo>().Name);
            base.OnStart();
        }

        protected override void OnSleep()
        {
            Akavache.BlobCache.Shutdown().Wait();
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMoneyTransferGuideService, MoneyTransferGuideService>();
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<CustomNavPage>();
            containerRegistry.RegisterSingleton<INavigationService, PageNavigationService>();
            containerRegistry.RegisterSingleton<IPageDialogService, PageDialogService>();
            containerRegistry.RegisterDialog<MoneyTransferInfoView, MoneyTransferInfoViewModel>();
            containerRegistry.RegisterDialog<MoneyTransferSystemsView, MoneyTransferSystemsViewModel>();
        }
    }
}
