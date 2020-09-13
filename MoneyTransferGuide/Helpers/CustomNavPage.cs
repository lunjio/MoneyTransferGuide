using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XF.Material.Forms.UI;

namespace MoneyTransferGuide.Helpers
{
    public partial class CustomNavPage : MaterialNavigationPage
    {
        public CustomNavPage(Xamarin.Forms.Page rootPage) : base(rootPage)
        {
            On<iOS>().SetUseSafeArea(true);
            On<iOS>().SetPrefersLargeTitles(false);
        }
    }
}
