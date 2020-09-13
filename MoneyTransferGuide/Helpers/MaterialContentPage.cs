using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using XF.Material.Forms;
using XF.Material.Forms.UI;

namespace MoneyTransferGuide.Helpers
{
    public class MaterialContentPage : ContentPage
    {
        public MaterialContentPage()
        {
            this.SetBinding(TitleProperty, "Title");
            MaterialNavigationPage.SetAppBarTitleTextAlignment(this, TextAlignment.Center);
            MaterialNavigationPage.SetAppBarColor(this, Material.Color.Primary);
            MaterialNavigationPage.SetStatusBarColor(this, Material.Color.Secondary);
            On<iOS>().SetUseSafeArea(true);
            On<iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);
        }

        public MaterialContentPage(bool setUseSafeArea, LargeTitleDisplayMode largeTitleDisplayMode)
        {
            On<iOS>().SetUseSafeArea(setUseSafeArea);
            On<iOS>().SetLargeTitleDisplay(largeTitleDisplayMode);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
