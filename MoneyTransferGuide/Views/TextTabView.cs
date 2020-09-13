using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;

namespace MoneyTransferGuide.Views
{
    public class TextTabView : ContentView
    {
        public TextTabView()
        {
            var textLabel = new MaterialLabel()
            {
                TextType = TextType.Html, FontSize = new OnIdiom<double>() { Phone = 18, Tablet = 20}
            };
            textLabel.SetBinding(Label.TextProperty, "Text");
            Content = new ScrollView()
            {
                Content = textLabel
            };
        }
    }
}