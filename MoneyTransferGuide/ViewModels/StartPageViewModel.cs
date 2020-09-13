using System;
using System.Collections.Generic;
using System.Text;
using Prism.Navigation;

namespace MoneyTransferGuide.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        public StartPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await NavigationService.NavigateAsync("/CustomNavPage/MainPage");
            base.OnNavigatedTo(parameters);
        }
    }
}
