using System;
using Xamarin.Forms;

namespace CarbonFootprint
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void SettingsButton(object _sender, EventArgs _e)
        {
            await Navigation.PushAsync(new SettingScreen());
        }
    }
}