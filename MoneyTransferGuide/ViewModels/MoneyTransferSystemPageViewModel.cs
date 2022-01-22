using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using MoneyTransferGuide.Helpers;
using MoneyTransferGuide.Model;
using MoneyTransferGuide.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Sharpnado.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace MoneyTransferGuide.ViewModels
{
    public class MoneyTransferSystemPageViewModel : ViewModelBase
    {
        private int _selectedInfIndex;
        public int SelectedInfIndex
        {
            get => _selectedInfIndex;
            set
            {
                SetProperty(ref _selectedInfIndex, value);
            }
        }

        private MoneyTransferSystem _moneyTransferSystem;
        public MoneyTransferSystem MoneyTransferSystem
        {
            get => _moneyTransferSystem;
            set
            {
                SetProperty(ref _moneyTransferSystem, value);
            }
        }

        private ObservableCollection<MoneyTransferSystemInformation> _contactSystemInf;
        public ObservableCollection<MoneyTransferSystemInformation> ContactSystemInf
        {
            get => _contactSystemInf;
            set
            {
                SetProperty(ref _contactSystemInf, value);
            }
        }

        public DelegateCommand<string> UrlTappedCommand { get;  }

        private IMoneyTransferGuideService MoneyTransferGuideService { get; }

        public MoneyTransferSystemPageViewModel(INavigationService navigationService,
            IMoneyTransferGuideService moneyTransferGuideService) : base(navigationService)
        {
            MoneyTransferGuideService = moneyTransferGuideService;
            UrlTappedCommand = new DelegateCommand<string>(
                async (adress) => await Browser.OpenAsync(new Uri(adress), BrowserLaunchMode.SystemPreferred)
                    .ConfigureAwait(false));

            ContactSystemInf = new ObservableCollection<MoneyTransferSystemInformation>();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            MoneyTransferSystem = (MoneyTransferSystem)parameters[KnownNavigationParameters.XamlParam];
            TaskMonitor.Create(InitializeAsync(null));
            base.OnNavigatedTo(parameters);
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            if (MoneyTransferSystem != null)
            {
                MoneyTransferSystem = await MoneyTransferGuideService.GetMoneyTransferSystemDetailAsync(MoneyTransferSystem.Id);
                ContactSystemInf.Add(new MoneyTransferSystemInformation("О системе", MoneyTransferSystem.Info));
                ContactSystemInf.Add(new MoneyTransferSystemInformation("Контакты", MoneyTransferSystem.Contacts));
                Title = MoneyTransferSystem.SystemName;

            }

            await base.InitializeAsync(parameters);
        }
    }
}
