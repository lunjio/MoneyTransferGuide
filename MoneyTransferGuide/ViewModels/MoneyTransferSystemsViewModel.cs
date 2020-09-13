using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MoneyTransferGuide.Model;
using MoneyTransferGuide.Services;
using Prism.Commands;
using Prism.Navigation;
using Sharpnado.Tasks;

namespace MoneyTransferGuide.ViewModels
{
    public class MoneyTransferSystemsViewModel : ViewModelBase
    {

        private MoneyTransferSystem _selectedMoneyTransferSystems;
        public MoneyTransferSystem SelectedMoneyTransferSystem
        {
            get => _selectedMoneyTransferSystems;
            set
            {
                SetProperty(ref _selectedMoneyTransferSystems, value);
            }
        }

        private List<MoneyTransferSystem> _moneyTransferSystems;
        public List<MoneyTransferSystem> MoneyTransferSystems
        {
            get => _moneyTransferSystems;
            set
            {
                SetProperty(ref _moneyTransferSystems, value);
            }
        }

        public DelegateCommand RefreshCommand { get; }

        private IMoneyTransferGuideService MoneyTransferGuideService { get; }

        public MoneyTransferSystemsViewModel(INavigationService navigationService, IMoneyTransferGuideService moneyTransferGuideService) : base(navigationService)
        {
            MoneyTransferGuideService = moneyTransferGuideService;
            TaskMonitor.Create(InitializeAsync(null));
            RefreshCommand = new DelegateCommand(() => TaskMonitor.Create(InitializeAsync(null)));
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            IsBusy = true;
            MoneyTransferSystems = await MoneyTransferGuideService.GetMoneyTransferSystemsAsync();
            IsBusy = false;
        }
    }
}
