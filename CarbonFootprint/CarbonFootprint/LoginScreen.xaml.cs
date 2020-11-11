using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarbonFootprint.DataCollection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarbonFootprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginScreen : ContentPage
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void LoginClicked(object _sender, EventArgs _e)
        {
            if(Username.Text.ToLower() == "admin")
                if (Password.Text.ToLower() == "admin")
                    MoveToNewPage();
        }

        private void MoveToNewPage()
        {
            if (Jsonhandler.Instance.CheckIfFileExists("userdata.json"))
                App.Current.MainPage = new NavigationPage(new HomeScreen());
            else
                App.Current.MainPage = new NavigationPage(new SettingScreen());
        }
    }
}