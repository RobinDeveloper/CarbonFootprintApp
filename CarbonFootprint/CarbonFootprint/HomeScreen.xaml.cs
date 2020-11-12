﻿using System;
using System.Collections.Generic;
using CarbonFootprint.DataCollection;
using CarbonFootprint.utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarbonFootprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeScreen : ContentPage
    {
        private UserData m_UserData;
        
        public HomeScreen()
        {
            InitializeComponent();

            LabelField();
        }

        private void LabelField()
        {
            m_UserData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");
            string nameFieldText = $"Hi Robin, \n Weclome to your carbon footprint homepage";
            NameLabel.Text = nameFieldText;
        }

        private async void PageButtonClicked(object _sender, EventArgs _e)
        {
            Button button = (Button) _sender;
            string classID = button.ClassId;

            switch (classID)
            {
                case "Transport":
                    await Navigation.PushAsync(new TransportScreen());
                    break;
                case "Food":
                    break;
                case "Products":
                    break;
                case "Regulars":
                    await Navigation.PushAsync(new EveryDayScreen());
                    break;
                case "Settings":
                    await Navigation.PushAsync(new SettingScreen());
                    break;
                default:
                    break;
            }
        }
    }
    
    public class ViewModel
    {
        public List<PieData> Data { get; set; }
        public List<PieData> Data2 { get; set; }
        public List<PieData> Data3 { get; set; }
        public List<PieData> Data4 { get; set; }
        
        private UserData m_UserData;
        public ViewModel()
        {
            if (!Jsonhandler.Instance.CheckIfFileExists("userdata.json")) return;
            m_UserData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");

            Data = new List<PieData>()
            {
                new PieData(m_UserData.PMNUDayScore.Positive, "Positive", Color.Green),
                new PieData(m_UserData.PMNUDayScore.Medium, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUDayScore.Negative, "Negative", Color.Red),
                new PieData(m_UserData.PMNUDayScore.Unkowm, "Unkown", Color.Gray)
            };
            
            Data2 = new List<PieData>()
            {
                new PieData(m_UserData.PMNUWeekScore.Positive, "Positive", Color.Green),
                new PieData(m_UserData.PMNUWeekScore.Medium, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUWeekScore.Negative, "Negative", Color.Red),
                new PieData(m_UserData.PMNUWeekScore.Unkowm, "Unkown", Color.Gray)
            };
            
            Data3 = new List<PieData>()
            {
                new PieData(m_UserData.PMNUMonthScore.Positive, "Positive", Color.Green),
                new PieData(m_UserData.PMNUMonthScore.Medium, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUMonthScore.Negative, "Negative", Color.Red),
                new PieData(m_UserData.PMNUMonthScore.Unkowm, "Unkown", Color.Gray)
            };
            
            Data4 = new List<PieData>()
            {
                new PieData(m_UserData.PMNUYearScore.Positive, "Positive", Color.Green),
                new PieData(m_UserData.PMNUYearScore.Medium, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUYearScore.Negative, "Negative", Color.Red),
                new PieData(m_UserData.PMNUYearScore.Unkowm, "Unkown", Color.Gray)
            };
        }
    }

    public class PieData
    {
        public int Value { get; set; }
        public string Rating { get; set; }
        public Color Color { get; set; }

        public PieData(int _value, string _rating, Color _color)
        {
            Value = _value;
            Rating = _rating;
            Color = _color;
        }
    }
}