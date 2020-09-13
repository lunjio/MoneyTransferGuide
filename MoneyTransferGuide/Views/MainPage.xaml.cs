using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyTransferGuide.Helpers;
using MoneyTransferGuide.Model;
using MoneyTransferGuide.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MoneyTransferGuide.Views
{
    public partial class MainPage : MaterialContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            On<iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);
        }

    }
}