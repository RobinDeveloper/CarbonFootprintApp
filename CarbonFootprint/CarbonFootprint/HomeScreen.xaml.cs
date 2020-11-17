using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using CarbonFootprint.DataCollection;
using CarbonFootprint.utilities;
using Syncfusion.SfChart.XForms;

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
        private DateCollected m_DateTime;

        public HomeScreen()
        {
            InitializeComponent();

            LabelField();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            m_UserData = Jsonhandler.Instance.CheckIfFileExists("userdata.json") ? Jsonhandler.Instance.RequestObject<UserData>("userdata.json") : new UserData();

            HandleChartsBinding();
            HandleDate();
        }
        private void HardReset()
        {
            UserData userData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");
            userData.PMNUDayScore = new PMNUScore(1,1,1,1);
            userData.PMNUWeekScore = new PMNUScore(1,1,1,1);
            userData.PMNUMonthScore =new PMNUScore(1,1,1,1);
            userData.PMNUYearScore = new PMNUScore(1,1,1,1);
            
            userData.FoodScore = new PMNUScore(1,1,1,1);
            userData.EverydayScore = new PMNUScore(1,1,1,1);
            userData.TransportScore = new PMNUScore(1,1,1,1);
            userData.ProdcutScore = new PMNUScore(1,1,1,1);
            Jsonhandler.Instance.UploadJson("userdata.json", userData);
        }
        
        private void HandleDate()
        {
            m_DateTime = Jsonhandler.Instance.CheckIfFileExists("date.json") ? Jsonhandler.Instance.RequestObject<DateCollected>("date.json") : new DateCollected();
            GeneralAppliances appliances = Jsonhandler.Instance.CheckIfFileExists("appliences.json")
                ? Jsonhandler.Instance.RequestObject<GeneralAppliances>("appliences.json")
                : null;
            if (m_DateTime.LastCheckedDay != DateTime.Today)
            {
                m_UserData.PMNUDayScore = new PMNUScore();
                if (appliances != null)
                    m_UserData.PMNUDayScore.Unkowm += appliances.PetValue;
                m_DateTime.LastCheckedDay = DateTime.Today;
            }

            if (!DatesAreInTheSameWeek(m_DateTime.LastCheckedWeek, DateTime.Today))
            {
                m_UserData.PMNUWeekScore = new PMNUScore();
                m_DateTime.LastCheckedWeek = DateTime.Today;
            }

            if (!DatesAreInTheSameMonth(m_DateTime.LastCheckedMonth, DateTime.Today))
            {
                m_UserData.PMNUMonthScore = new PMNUScore();
                m_DateTime.LastCheckedMonth = DateTime.Today;
            }

            if (!DatesAreInTheSameYear(m_DateTime.LastCheckedYear, DateTime.Today))
            {
                m_UserData.PMNUYearScore = new PMNUScore();
                m_DateTime.LastCheckedYear = DateTime.Today;
            }
            
            Jsonhandler.Instance.UploadJson("userdata.json", m_UserData);
            Jsonhandler.Instance.UploadJson("data.json", m_DateTime);
        }
        
        //https://stackoverflow.com/questions/25795254/check-if-a-datetime-is-in-same-week-as-other-datetime/38792243
        private bool DatesAreInTheSameWeek(DateTime _date1, DateTime _date2)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = _date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(_date1));
            var d2 = _date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(_date2));

            return d1 == d2;
        }
        
        private bool DatesAreInTheSameMonth(DateTime _date1, DateTime _date2)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = _date1.Date.AddDays(-1 * (int)cal.GetDayOfMonth(_date1));
            var d2 = _date2.Date.AddDays(-1 * (int)cal.GetDayOfMonth(_date2));

            return d1 == d2;
        }
        
        private bool DatesAreInTheSameYear(DateTime _date1, DateTime _date2)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = _date1.Date.AddDays(-1 * (int)cal.GetDayOfYear(_date1));
            var d2 = _date2.Date.AddDays(-1 * (int)cal.GetDayOfYear(_date2));

            return d1 == d2;
        }
        
        private void HandleChartsBinding()
        {
            Chart.BindingContext = new ViewModel();
            Chart2.BindingContext = new ViewModel();
            Chart3.BindingContext = new ViewModel();
            Chart4.BindingContext = new ViewModel();
        }
        
        private void LabelField()
        {
            m_UserData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");
            string nameFieldText = $"Hi {m_UserData.Name},";
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
                    await Navigation.PushAsync(new FoodScreen());
                    break;
                case "Products":
                    await Navigation.PushAsync(new ProductScreen());
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
        public ObservableCollection<PieData> Data { get; set; }
        public ObservableCollection<PieData> Data2 { get; set; }
        public ObservableCollection<PieData> Data3 { get; set; }
        public ObservableCollection<PieData> Data4 { get; set; }
        
        private UserData m_UserData;
        public ViewModel()
        {
            //HardReset();
            if (!Jsonhandler.Instance.CheckIfFileExists("userdata.json")) return;
            m_UserData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");

            Data = new ObservableCollection<PieData>()
            {
                new PieData(m_UserData.PMNUDayScore.Positive, "Positive", Color.Green),
                new PieData(m_UserData.PMNUDayScore.Medium, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUDayScore.Negative, "Negative", Color.Red),
                new PieData(m_UserData.PMNUDayScore.Unkowm, "Unkown", Color.Gray)
            };
            
            Data2 = new ObservableCollection<PieData>()
            {
                new PieData(m_UserData.PMNUWeekScore.Positive, "Positive", Color.Green),
                new PieData(m_UserData.PMNUWeekScore.Medium, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUWeekScore.Negative, "Negative", Color.Red),
                new PieData(m_UserData.PMNUWeekScore.Unkowm, "Unkown", Color.Gray)
            };
            
            Data3 = new ObservableCollection<PieData>()
            {
                new PieData(m_UserData.PMNUMonthScore.Positive, "Positive", Color.Green),
                new PieData(m_UserData.PMNUMonthScore.Medium, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUMonthScore.Negative, "Negative", Color.Red),
                new PieData(m_UserData.PMNUMonthScore.Unkowm, "Unkown", Color.Gray)
            };
            
            Data4 = new ObservableCollection<PieData>()
            {
                new PieData(m_UserData.PMNUYearScore.Positive, "Positive", Color.Green),
                new PieData(m_UserData.PMNUYearScore.Medium, "Medium", Color.Yellow),
                new PieData(m_UserData.PMNUYearScore.Negative, "Negative", Color.Red),
                new PieData(m_UserData.PMNUYearScore.Unkowm, "Unkown", Color.Gray)
            };
        }
        
        private void HardReset()
        {
            UserData userData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");
            userData.PMNUDayScore = new PMNUScore(1,1,1,1);
            userData.PMNUWeekScore = new PMNUScore(1,1,1,1);
            userData.PMNUMonthScore =new PMNUScore(1,1,1,1);
            userData.PMNUYearScore = new PMNUScore(1,1,1,1);
            
            userData.FoodScore = new PMNUScore(1,1,1,1);
            userData.EverydayScore = new PMNUScore(1,1,1,1);
            userData.TransportScore = new PMNUScore(1,1,1,1);
            userData.ProdcutScore = new PMNUScore(1,1,1,1);
            Jsonhandler.Instance.UploadJson("userdata.json", userData);
        }
    }

    public class PieData
    {
        public double Value { get; set; }
        public string Rating { get; set; }
        public Color Color { get; set; }

        public PieData(double _value, string _rating, Color _color)
        {
            Value = _value;
            Rating = _rating;
            Color = _color;
        }
    }
}