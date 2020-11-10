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
            double distanceRoute;

            if (!Double.TryParse(BicycleDistance.Text, out distanceRoute))
            {
                DisplayAlert("Warning", "Place a distance", "OK");
            }
            else
            {
                m_CarbonBicycle = 0;
                TotalSumCarbon(m_CarbonBicycle, PositivityRating.Positive);
                UploadData();
            }
        }

        private void AddTrainDistance(object _sender, EventArgs _e)
        {
            double distanceTrain;

            if (!double.TryParse(TrainDistance.Text, out distanceTrain))
            {
                DisplayAlert("Warning", "Place a distance", "OK");
            }
            else
            {
                m_CarbonTrain = 0;
                TotalSumCarbon(m_CarbonTrain, PositivityRating.Positive);
                UploadData();
            }
        }

        private void AddBusDistance(object _sender, EventArgs _e)
        {
            float distanceBus;

            if(!float.TryParse(BusDistance.Text, out distanceBus))
            {
                DisplayAlert("Warning", "Place a distance", "OK");
            }
            else
            {
                m_CarbonBus = 0.137f * distanceBus;
                TotalSumCarbon(m_CarbonBus, PositivityRating.Medium);
                UploadData();
            }
        }

        private void AddCarDistance(object _sender, EventArgs _e)
        {
            float distanceCar;
            int carConsumption = m_UserData.Car.GasMilage;

            if(!float.TryParse(CarDistance.Text, out distanceCar))
            {
                DisplayAlert("Warning", "Place a distance", "OK");
            }
            else
            {
                switch (m_UserData.Car.CarEnergyType)
                {
                    case Car.CarType.Hybrid:
                        m_CarbonCar = 0.326f * (distanceCar / carConsumption);
                        TotalSumCarbon(m_CarbonCar, PositivityRating.Medium);
                        UploadData();
                        break;
                    case Car.CarType.Gas:
                        m_CarbonCar = 0.652f * (distanceCar / carConsumption);
                        TotalSumCarbon(m_CarbonCar, PositivityRating.Negative);
                        UploadData();
                        break;
                    case Car.CarType.Diesel:
                        m_CarbonCar = 0.72f * (distanceCar / carConsumption);
                        TotalSumCarbon(m_CarbonCar, PositivityRating.Negative);
                        UploadData();
                        break;
                    case Car.CarType.Electric:
                        m_CarbonCar = 0 * (distanceCar / carConsumption);
                        TotalSumCarbon(m_CarbonCar, PositivityRating.Positive);
                        UploadData();
                        break;
                }
            }
        }

        private void TotalSumCarbon(float _carbon, PositivityRating _rating)
        {
            switch(_rating)
            {
                case PositivityRating.Positive:
                    m_UserData.PMNUDayScore = new Tuple<int, int, int, int>
                        (
                            (int)_carbon,
                            m_UserData.PMNUDayScore.Item2,
                            m_UserData.PMNUDayScore.Item3,
                            m_UserData.PMNUDayScore.Item4
                        );
                    break;
                case PositivityRating.Medium:
                    m_UserData.PMNUDayScore = new Tuple<int, int, int, int>
                        (
                            m_UserData.PMNUDayScore.Item1,
                           (int)_carbon,
                            m_UserData.PMNUDayScore.Item3,
                            m_UserData.PMNUDayScore.Item4
                        );
                    break;
                case PositivityRating.Negative:
                    m_UserData.PMNUDayScore = new Tuple<int, int, int, int>
                       (
                           m_UserData.PMNUDayScore.Item1,
                            m_UserData.PMNUDayScore.Item2,
                          (int)_carbon,
                           m_UserData.PMNUDayScore.Item4
                       );
                    break;
                case PositivityRating.Unkown:
                    m_UserData.PMNUDayScore = new Tuple<int, int, int, int>
                       (
                           m_UserData.PMNUDayScore.Item1,
                            m_UserData.PMNUDayScore.Item2,
                           m_UserData.PMNUDayScore.Item3,
                           (int)_carbon
                       );
                    break;
            }

            float totalCarbon = _carbon;
            SumCarbon.Text = "Your emission for the previous travel was: " + totalCarbon;
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
            await Navigation.PushAsync(new MainPage());
        }
    }
}