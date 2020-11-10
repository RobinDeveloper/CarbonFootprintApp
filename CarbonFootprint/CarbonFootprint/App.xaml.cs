﻿using System;
using CarbonFootprint.DataCollection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarbonFootprint.DataCollection;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CarbonFootprint
{
    public partial class App : Application
    {
        private Jsonhandler m_Jsonhandler;
        public App()
        {
            Jsonhandler handlr = new Jsonhandler();
            InitializeComponent();
            if (Jsonhandler.Instance.CheckIfFileExists("userdata.json"))
                MainPage = new NavigationPage(new TransportScreen());
            else
                MainPage = new NavigationPage(new SettingScreen());
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