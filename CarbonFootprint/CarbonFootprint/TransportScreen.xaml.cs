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
        private double m_CarbonBicycle;
        private double m_CarbonTrain;
        private double m_CarbonBus;
        private double m_CarbonCar;

        public TransportScreen()
        {
            InitializeComponent();
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
                TotalSumCarbon(m_CarbonBicycle);
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
                TotalSumCarbon(m_CarbonTrain);
            }
        }

        private void AddBusDistance(object _sender, EventArgs _e)
        {
            double distanceBus;

            if(!double.TryParse(BusDistance.Text, out distanceBus))
            {
                DisplayAlert("Warning", "Place a distance", "OK");
            }
            else
            {
                m_CarbonBus = 0.137 * distanceBus;
                TotalSumCarbon(m_CarbonBus);
            }
        }

        private void AddCarDistance(object _sender, EventArgs _e)
        {
            double distanceCar;
            int carConsumption = Convert.ToInt32(InputConsumption.Text);

            if(!double.TryParse(CarDistance.Text, out distanceCar))
            {
                DisplayAlert("Warning", "Place a distance", "OK");
            }
            else
            {
                if(InputCartype.Text == "Hybrid")
                {
                    m_CarbonCar = 0.326 * (distanceCar / carConsumption);
                    TotalSumCarbon(m_CarbonCar);
                }
                else if(InputCartype.Text == "Gas")
                {
                    m_CarbonCar = 0.652 * (distanceCar / carConsumption);
                    TotalSumCarbon(m_CarbonCar);
                }
                else if(InputCartype.Text == "Diesel")
                {
                    m_CarbonCar = 0.72 * (distanceCar / carConsumption);
                    TotalSumCarbon(m_CarbonCar);
                }
                else if(InputCartype.Text == "Electric")
                {
                    m_CarbonCar = 0 * (distanceCar / carConsumption);
                    TotalSumCarbon(m_CarbonCar);
                }
            }
        }

        private void TotalSumCarbon(double Carbon)
        {
            double TotalCarbon = Carbon;
            SumCarbon.Text = "Your emission for the previous travel was: " + TotalCarbon;
        }
    }
}