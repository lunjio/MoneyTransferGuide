using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoneyTransferGuide.Model;
using MoneyTransferGuide.Services;
using Prism.Commands;
using Prism.Navigation;
using Sharpnado.Tasks;
using Xamarin.Essentials;

namespace MoneyTransferGuide.ViewModels
{
    public class MoneyTransferSystemPageViewModel : ViewModelBase
    {
        private int _selectedViewModelIndex;
        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set
            {
                SetProperty(ref _selectedViewModelIndex, value);
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

        private TextTabViewModel _infoTextTabViewModel;
        public TextTabViewModel InfoTextTabViewModel
        {
            get => _infoTextTabViewModel;
            set
            {
                SetProperty(ref _infoTextTabViewModel, value);
            }
        }

        private TextTabViewModel _contactsTextTabViewModel;
        public TextTabViewModel ContactsTextTabViewModel
        {
            get => _contactsTextTabViewModel;
            set
            {
                SetProperty(ref _contactsTextTabViewModel, value);
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
                InfoTextTabViewModel =  new TextTabViewModel(MoneyTransferSystem.Info);
                ContactsTextTabViewModel = new TextTabViewModel(MoneyTransferSystem.Contacts);
                Title = MoneyTransferSystem.SystemName;
            }


            await base.InitializeAsync(parameters);
        }

    }
}
