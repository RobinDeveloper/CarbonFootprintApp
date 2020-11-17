using CarbonFootprint.DataCollection;
using CarbonFootprint.utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarbonFootprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransportScreen : ContentPage
    {
        private float m_CarbonBicycle;
        private float m_CarbonTrain;
        private float m_CarbonBus;
        private float m_CarbonCar;

        private UserData m_UserData;

        public TransportScreen()
        {
            InitializeComponent();
            m_UserData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");
        }

        private void AddBicycleDistance(object _sender, EventArgs _e)
        {
            double distanceBicycle = DistanceCheck(BicycleDistance.Text, "Distance is an incorrect value");
            m_CarbonBicycle = (float) CalculateCarbonExhaust(0, distanceBicycle);

            TotalSumCarbon(m_CarbonBicycle, PositivityRating.Positive);
        }

        private void AddTrainDistance(object _sender, EventArgs _e)
        {
            double distanceTrain = DistanceCheck(TrainDistance.Text, "Distance is an incorrect value");
            m_CarbonTrain = (float) CalculateCarbonExhaust(0, distanceTrain);
            TotalSumCarbon(m_CarbonTrain, PositivityRating.Positive);
        }

        private void AddBusDistance(object _sender, EventArgs _e)
        {
            double distanceBus = DistanceCheck(BusDistance.Text, "Distance is an incorrect value");
            m_CarbonBus = (float) CalculateCarbonExhaust(0.137, distanceBus);
            TotalSumCarbon(m_CarbonBus, PositivityRating.Medium);
        }

        private void AddCarDistance(object _sender, EventArgs _e)
        {
            double distanceCar = DistanceCheck(CarDistance.Text, "Distance is an incorrect value");
            int carConsumption = m_UserData.Car.GasMilage;


                switch (m_UserData.Car.CarEnergyType)
                {
                    case Car.CarType.Hybrid:
                        m_CarbonCar = (float)CalculateCarbonExhaust(0.326, (distanceCar / carConsumption));
                        TotalSumCarbon(m_CarbonCar, PositivityRating.Medium);
                        break;
                    case Car.CarType.LPG:
                        m_CarbonCar = (float)CalculateCarbonExhaust(0.454, (distanceCar / carConsumption));
                        TotalSumCarbon(m_CarbonCar, PositivityRating.Negative);
                        break;
                    case Car.CarType.Diesel:
                        m_CarbonCar = (float)CalculateCarbonExhaust(0.72, (distanceCar / carConsumption));
                        TotalSumCarbon(m_CarbonCar, PositivityRating.Negative); 
                        break;
                    case Car.CarType.Electric:
                        m_CarbonCar = (float)CalculateCarbonExhaust(0, (distanceCar / carConsumption));
                        TotalSumCarbon(m_CarbonCar, PositivityRating.Positive); 
                        break;
                }
            
        }
        
        private double DistanceCheck(string _text, string _warningTitle)
        {
            double result;
            
            if (double.TryParse(_text, out result))
                return result;
            
            
            DisplayAlert(_warningTitle, "Place a distance", "OK");
            return Double.NaN;
        }

        private double CalculateCarbonExhaust(double _multiplyValue, double _distance)
        {
            return _multiplyValue * _distance;
        }
        
        private void TotalSumCarbon(float _carbon, PositivityRating _rating)
        {
            switch(_rating)
            {
                case PositivityRating.Positive:
                    m_UserData.PMNUDayScore.Positive += _carbon;
                    m_UserData.PMNUWeekScore.Positive += _carbon;
                    m_UserData.PMNUMonthScore.Positive += _carbon;
                    m_UserData.PMNUYearScore.Positive += _carbon;
                    m_UserData.TransportScore.Positive += _carbon;
                    break;
                case PositivityRating.Medium:
                    m_UserData.PMNUDayScore.Medium += _carbon;
                    m_UserData.PMNUWeekScore.Medium += _carbon;
                    m_UserData.PMNUMonthScore.Medium += _carbon;
                    m_UserData.PMNUYearScore.Medium += _carbon;
                    m_UserData.TransportScore.Medium += _carbon;    
                    break;
                case PositivityRating.Negative:
                    m_UserData.PMNUDayScore.Negative += _carbon;
                    m_UserData.PMNUWeekScore.Negative += _carbon;
                    m_UserData.PMNUMonthScore.Negative += _carbon;
                    m_UserData.PMNUYearScore.Negative += _carbon;
                    m_UserData.TransportScore.Negative += _carbon;    
                    break;
                case PositivityRating.Unkown:
                    m_UserData.PMNUDayScore.Unkowm += _carbon;
                    m_UserData.PMNUWeekScore.Unkowm += _carbon;
                    m_UserData.PMNUMonthScore.Unkowm += _carbon;
                    m_UserData.PMNUYearScore.Unkowm += _carbon;
                    m_UserData.TransportScore.Unkowm += _carbon;  
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_rating), _rating, null);
            }

            float totalCarbon = _carbon;
            SumCarbon.Text = "Your emission for the previous travel was: " + totalCarbon.ToString();
            UploadData();
        }

        private void UploadData()
        {
            Jsonhandler.Instance.UploadJson("userdata.json", m_UserData);
            Chart.BindingContext = new ViewmodelTransport();
        }

        private async void GoToMainScreen(object _sender, EventArgs _e)
        {
            await Navigation.PushAsync(new HomeScreen());
        }
    }
    
    public class ViewmodelTransport
    {
        public ObservableCollection<PieDataProduct> Data { get; set; }
        
        private UserData m_UserData;

        public ViewmodelTransport()
        {
            m_UserData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");
            
            Data = new ObservableCollection<PieDataProduct>()
            {
                new PieDataProduct(m_UserData.TransportScore.Positive, "Positive", Color.Green),
                new PieDataProduct(m_UserData.TransportScore.Medium, "Medium", Color.Yellow),
                new PieDataProduct(m_UserData.TransportScore.Negative, "Negative", Color.Red),
                new PieDataProduct(m_UserData.TransportScore.Unkowm, "Unkown", Color.Gray)
            };
        }
    }
}