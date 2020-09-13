using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyTransferGuide.Views;
using MoneyTransferGuide.Model;
using MoneyTransferGuide.Services;
using Prism;
using Prism.Common;
using Prism.Ioc;
using Sharpnado.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace MoneyTransferGuide.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private int _selectedViewModelIndex;
        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set
            {
                if (value == 0)
                {
                    Title = "Денежные переводы";
                }
                else if(value == 1)
                {
                    Title = "Денежные системы";
                }
                SetProperty(ref _selectedViewModelIndex, value);
            }
        }

        private MoneyTransferInfoViewModel _moneyTransferInfoViewModel;
        public MoneyTransferInfoViewModel MoneyTransferInfoViewModel
        {
            get => _moneyTransferInfoViewModel;
            set => SetProperty(ref _moneyTransferInfoViewModel, value);
        }

        private MoneyTransferSystemsViewModel _moneyTransferSystemsViewModel;
        public MoneyTransferSystemsViewModel MoneyTransferSystemsViewModel
        {
            get => _moneyTransferSystemsViewModel;
            set => SetProperty(ref _moneyTransferSystemsViewModel, value);
        }

        public DelegateCommand<string> UrlTappedCommand { get; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Денежные переводы";
            var container = PrismApplicationBase.Current.Container;
            MoneyTransferInfoViewModel = container.Resolve<MoneyTransferInfoViewModel>();
            MoneyTransferSystemsViewModel = container.Resolve<MoneyTransferSystemsViewModel>();
            UrlTappedCommand = new DelegateCommand<string>(
                async (adress) => await Browser.OpenAsync(new Uri(adress), BrowserLaunchMode.SystemPreferred)
                    .ConfigureAwait(false));
        }

    }
}
