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
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace MoneyTransferGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoneyTransferInfoView : ContentView
    {
        public MoneyTransferInfoView()
        {
            InitializeComponent();

            MaterialConfirmationDialog.SetDialogTitle(CountryFrom, "Страна отправления");
            MaterialConfirmationDialog.SetDialogConfirmingText(CountryFrom, "Ок");
            MaterialConfirmationDialog.SetDialogDismissiveText(CountryFrom, "Отмена");

            MaterialConfirmationDialog.SetDialogTitle(CountryTo, "Страна получения");
            MaterialConfirmationDialog.SetDialogConfirmingText(CountryTo, "Ок");
            MaterialConfirmationDialog.SetDialogDismissiveText(CountryTo, "Отмена");
        }

        private void Layout_OnLayoutChanged(object sender, EventArgs e)
        {
            Expander.ForceUpdateSize();
        }
    }
}