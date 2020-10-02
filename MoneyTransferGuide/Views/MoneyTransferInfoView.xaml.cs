using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyTransferGuide.Helpers;
using Prism;
using Prism.Events;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTransferGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoneyTransferInfoView : ContentView
    {
        public MoneyTransferInfoView()
        {
            InitializeComponent();
        }

        private void Layout_OnLayoutChanged(object sender, EventArgs e)
        {
            Expander.ForceUpdateSize();
        }
    }
}