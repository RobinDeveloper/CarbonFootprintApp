using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarbonFootprint.DataCollection;
using CarbonFootprint.utilities;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarbonFootprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DebugValues : ContentPage
    {
        private UserData m_UserData;
        
        public DebugValues()
        {
            InitializeComponent();
            DeserializeJson();
            UpdateLabels();
        }

        private void DeserializeJson()
        {
            m_UserData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");
        }

        private void UpdateLabels()
        {
            NameField.Text = m_UserData.Name;
            CarName.Text = m_UserData.Car.Name;
            CarAge.Text = m_UserData.Car.Age.ToString();
            CarBuildYear.Text = m_UserData.Car.BuildYear.ToString();
            GasMilage.Text = m_UserData.Car.GasMilage.ToString();
            CarType.Text = m_UserData.Car.CarEnergyType.ToString();
            HouseEnergy.Text = m_UserData.House.EneryEfficiancy.ToString();
            CubicVolume.Text =
                $"{m_UserData.House.CubicMeters.Item1.ToString()} : {m_UserData.House.CubicMeters.Item2.ToString()} : {m_UserData.House.CubicMeters.Item3.ToString()}";
        }
    }
}