using CarbonFootprint.DataCollection;
using CarbonFootprint.utilities;
using System;
using System.Collections.Generic;
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
            PopulateUserData();
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
                    m_UserData.PMNUDayScore = new Tuple<int, int, int, int>
                        (
                            m_UserData.PMNUDayScore.Item1 + (int)_carbon,
                            m_UserData.PMNUDayScore.Item2,
                            m_UserData.PMNUDayScore.Item3,
                            m_UserData.PMNUDayScore.Item4
                        );
                    break;
                case PositivityRating.Medium:
                    m_UserData.PMNUDayScore = new Tuple<int, int, int, int>
                        (
                            m_UserData.PMNUDayScore.Item1,
                            m_UserData.PMNUDayScore.Item2 + (int)_carbon,
                            m_UserData.PMNUDayScore.Item3,
                            m_UserData.PMNUDayScore.Item4
                        );
                    break;
                case PositivityRating.Negative:
                    m_UserData.PMNUDayScore = new Tuple<int, int, int, int>
                       (
                           m_UserData.PMNUDayScore.Item1,
                           m_UserData.PMNUDayScore.Item2,
                           m_UserData.PMNUDayScore.Item3 + (int)_carbon,
                           m_UserData.PMNUDayScore.Item4
                       );
                    break;
                case PositivityRating.Unkown:
                    m_UserData.PMNUDayScore = new Tuple<int, int, int, int>
                       (
                           m_UserData.PMNUDayScore.Item1,
                           m_UserData.PMNUDayScore.Item2,
                           m_UserData.PMNUDayScore.Item3,
                           m_UserData.PMNUDayScore.Item4 + (int)_carbon
                       );
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
        }

        private void PopulateUserData()
        {
            if (m_UserData.PMNUDayScore == null)
                m_UserData.PMNUDayScore = new Tuple<int, int, int, int>(0, 0, 0, 0);

            if (m_UserData.PMNUWeekScore == null)
                m_UserData.PMNUWeekScore = new Tuple<int, int, int, int>(0, 0, 0, 0);

            if (m_UserData.PMNUMonthScore == null)
                m_UserData.PMNUMonthScore = new Tuple<int, int, int, int>(0, 0, 0, 0);

            if (m_UserData.PMNUYearScore == null)
                m_UserData.PMNUYearScore = new Tuple<int, int, int, int>(0, 0, 0, 0);
        }

        private async void GoToMainScreen(object _sender, EventArgs _e)
        {
            await Navigation.PushAsync(new HomeScreen());
        }
    }
}