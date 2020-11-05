using System;
using CarbonFootprint.DataCollection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CarbonFootprint
{
    public partial class App : Application
    {
        private Jsonhandler m_Jsonhandler;
        public App()
        {
            m_Jsonhandler = new Jsonhandler();
            InitializeComponent();

            MainPage = new SettingScreen();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}