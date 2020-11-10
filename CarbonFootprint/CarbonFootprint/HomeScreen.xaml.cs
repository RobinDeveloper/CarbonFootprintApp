using System;
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
                        
            PopulateUserData();
            
            Data = new List<PieData>()
            {
                new PieData(m_UserData.PMNUDayScore.Item1, "Positive", Color.Green),
                new PieData(m_UserData.PMNUDayScore.Item2, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUDayScore.Item3, "Negative", Color.Red),
                new PieData(m_UserData.PMNUDayScore.Item4, "Unkown", Color.Gray)
            };
            
            Data2 = new List<PieData>()
            {
                new PieData(m_UserData.PMNUWeekScore.Item1, "Positive", Color.Green),
                new PieData(m_UserData.PMNUWeekScore.Item2, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUWeekScore.Item3, "Negative", Color.Red),
                new PieData(m_UserData.PMNUWeekScore.Item4, "Unkown", Color.Gray)
            };
            
            Data3 = new List<PieData>()
            {
                new PieData(m_UserData.PMNUMonthScore.Item1, "Positive", Color.Green),
                new PieData(m_UserData.PMNUMonthScore.Item2, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUMonthScore.Item3, "Negative", Color.Red),
                new PieData(m_UserData.PMNUMonthScore.Item4, "Unkown", Color.Gray)
            };
            
            Data4 = new List<PieData>()
            {
                new PieData(m_UserData.PMNUYearScore.Item1, "Positive", Color.Green),
                new PieData(m_UserData.PMNUYearScore.Item2, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUYearScore.Item3, "Negative", Color.Red),
                new PieData(m_UserData.PMNUYearScore.Item4, "Unkown", Color.Gray)
            };
        }
        
        private void PopulateUserData()
        {
            if(m_UserData.PMNUDayScore == null)
                m_UserData.PMNUDayScore = new Tuple<int, int, int, int>(10,41,12,0);
    
            if(m_UserData.PMNUWeekScore == null)
                m_UserData.PMNUWeekScore = new Tuple<int, int, int, int>(12,65,122,0);
    
            if(m_UserData.PMNUMonthScore == null)
                m_UserData.PMNUMonthScore = new Tuple<int, int, int, int>(25,9,56,56);
    
            if(m_UserData.PMNUYearScore == null)
                m_UserData.PMNUYearScore = new Tuple<int, int, int, int>(87,50,77,10);
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