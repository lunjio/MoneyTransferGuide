using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoneyTransferGuide.Helpers;
using MoneyTransferGuide.Model;
using MoneyTransferGuide.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Sharpnado.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace MoneyTransferGuide.ViewModels
{
    public class MoneyTransferInfoViewModel : ViewModelBase
    {
        private IMaterialModalPage LoadingDialog { get; set; }

        private IMoneyTransferGuideService MoneyTransferGuideService { get; }

        private List<MoneyTransferInfoData> _moneySendQueryData = new List<MoneyTransferInfoData>();
        public List<MoneyTransferInfoData> MoneySendQueryData { get => _moneySendQueryData; set => SetProperty(ref _moneySendQueryData, value); }

        private List<Country> _countries = new List<Country>();
        public List<Country> Countries
        {
            get => _countries;
            set
            {
                SetProperty(ref _countries, value);
                UpdateTransformParameters();
            }
        }

        private Country _countryFrom;
        public Country CountryFrom
        {
            get => _countryFrom;
            set
            {
                SetProperty(ref _countryFrom, value);
                UpdateTransformParameters();
            }
        }

        private Country _countryTo;
        public Country CountryTo { get => _countryTo; set => SetProperty(ref _countryTo, value); }

        private string _transferParameters;
        public string TransferParameters { get => _transferParameters; set => SetProperty(ref _transferParameters, value); }

        private int _summ;
        public int Summ { get => _summ; set => SetProperty(ref _summ, value); }

        private string _currency;
        public string Currency { get => _currency; set => SetProperty(ref _currency, value); }

        private bool _isExpanded;
        public bool IsExpanded { get => _isExpanded; set => SetProperty(ref _isExpanded, value); }

        private bool _countryFromIsUnfilled;
        public bool CountryFromIsUnfilled { get => _countryFromIsUnfilled; set => SetProperty(ref _countryFromIsUnfilled, value); }

        private bool _countryToIsUnfilled;
        public bool CountryToIsUnfilled { get => _countryToIsUnfilled; set => SetProperty(ref _countryToIsUnfilled, value); }

        private bool _summIsUnfilled;
        public bool SummIsUnfilled { get => _summIsUnfilled; set => SetProperty(ref _summIsUnfilled, value); }

        public DelegateCommand GetMoneySendInfoCommand { get; }

        public DelegateCommand<string> UrlTappedCommand { get; }

        public DelegateCommand<object> OnParameterChangedCommand { get; }

        public Command<object> OnCountryFromChangedCommand { get; }

        public Command<object> OnCountryToChangedCommand { get; }

        public MoneyTransferInfoViewModel(INavigationService navigationService, IMoneyTransferGuideService moneyTransferGuideService) : base(navigationService)
        {
            IsExpanded = true;
            MoneyTransferGuideService = moneyTransferGuideService;
            TaskMonitor.Create(InitializeAsync(null));
            GetMoneySendInfoCommand = new DelegateCommand((() =>
            {
                TaskMonitor.Create(GetMoneySendInfoAsync);
            }));

            UrlTappedCommand = new DelegateCommand<string>(
                async (adress) => await Browser.OpenAsync(new Uri(adress), new BrowserLaunchOptions()
                {
                    PreferredToolbarColor = Material.Color.Secondary,
                    PreferredControlColor = Material.Color.Primary,
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Hide
                })
                    .ConfigureAwait(false));

            Currency = "RUR";

            OnParameterChangedCommand = new DelegateCommand<object>((o) => 
                ValidateErrors());

            OnCountryFromChangedCommand = new Command<object>((s) =>
            {
                CountryFromIsUnfilled = (CountryFrom == null);
            });

            OnCountryToChangedCommand = new Command<object>((s) =>
            {
                CountryToIsUnfilled = (CountryTo == null);

            });
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            await base.InitializeAsync(parameters);
            Countries = new List<Country>()
            {
                new Country() {Code = "RUS", Name = "Россия"},
                new Country() {Code = "UKR", Name = "Украина"},
                new Country() {Code = "AZE", Name = "Азербайджан"},
                new Country() {Code = "ARM", Name = "Армения"},
                new Country() {Code = "BLR", Name = "Белоруссия"},
                new Country() {Code = "GEO", Name = "Грузия"},
                new Country() {Code = "KAZ", Name = "Казахстан"},
                new Country() {Code = "KGZ", Name = "Киргизия"},
                new Country() {Code = "LVA", Name = "Латвия"},
                new Country() {Code = "MDA", Name = "Молдавия"},
                new Country() {Code = "TJK", Name = "Таджикистан"},
                new Country() {Code = "UZB", Name = "Узбекистан"},
                new Country() {Code = "USA", Name = "США"},
                new Country() {Code = "CZE", Name = "Чехия"},
            };
        }

        private async Task GetMoneySendInfoAsync()
        {
            if (!(CountryFrom == null || CountryTo == null))
            {
                LoadingDialog = await MaterialDialog.Instance.LoadingDialogAsync("Пожалуйста, подождите...").ConfigureAwait(false);
                MoneySendQueryData = await MoneyTransferGuideService.GetMoneyTransferInfoAsync(CountryFrom.Code, CountryTo.Code, Summ, Currency).ConfigureAwait(false);
                await MainThread.InvokeOnMainThreadAsync(async () => await LoadingDialog.DismissAsync().ConfigureAwait(false)).ConfigureAwait(false);
                IsExpanded = false;
                UpdateTransformParameters();
            }
            else
            {
                ValidateErrors();
            }
        }

        private void UpdateTransformParameters()
        {
            if (!(CountryFrom == null || CountryTo == null || Currency == null))
            {
                TransferParameters = $"{CountryFrom.Name} - {CountryTo.Name} {$"{Summ:##;(##);}"} {Currency}";
            }
        }

        public void ValidateErrors()
        {
            CountryFromIsUnfilled = (CountryFrom == null);
            CountryToIsUnfilled = (CountryTo == null);
        }
    }
}
