using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarbonFootprint.DataCollection;
using CarbonFootprint.utilities;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
// ReSharper disable HeapView.BoxingAllocation

namespace CarbonFootprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingScreen : ContentPage
    {
        private UserData m_UserData = new UserData();
        private Car m_CarData = new Car();
        private House m_HouseData = new House();

        private Dictionary<string, Car.CarType> m_NameToCarType = new Dictionary<string, Car.CarType>
        {
            {"Hybrid", Car.CarType.Hybrid},
            {"Gas", Car.CarType.Gas},
            {"Diesel", Car.CarType.Diesel},
            {"Electric", Car.CarType.Electric}
        };

        public SettingScreen()
        {
            InitializeComponent();

            foreach (string typeName in m_NameToCarType.Keys)
            {
                TypePicker.Items.Add(typeName);
            }
        }
        
        private void OnSubmit(object _sender, EventArgs _e)
        {
            CreateUserData();
            Jsonhandler.Instance.UploadJson("userdata.json", _toSerialize: m_UserData);
        }

        private void CreateUserData()
        {
            GetPersonalInformation();
            GetCarInformation();
            GetHouseInformation();
            m_UserData.Car = m_CarData;
            m_UserData.House = m_HouseData;
        }

        private void GetPersonalInformation()
        {
            if(NameField.Text != String.Empty)
                m_UserData.Name = NameField.Text;
        }

        private void GetCarInformation()
        {
            m_CarData.Name = CarName.Text;
            m_CarData.Age = ParseIntValue(CarAge.Text, "Invalid car age");
            m_CarData.BuildYear = ParseIntValue(Carbuild.Text, "Invalid build year");
            m_CarData.GasMilage = ParseIntValue(Carbuild.Text, "Invalid milage input");
            Enum.TryParse(TypePicker.SelectedItem.ToString(), out m_CarData.CarEnergyType);
        }

        private void GetHouseInformation()
        {
            char e = EnergyEfficiency.Text.ToLower()[0];
            if (e == 'a' || e == 'b' || e == 'c' || e == 'd' || e == 'e' || e == 'f')
                m_HouseData.EneryEfficiancy = e.ToString().ToUpper()[0];
            else
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Invalid energy rating", "please insert character from a-f", "ok");
                });
            m_HouseData.CubicMeters = GetCubicMeters(CubicVolume.Text);

            if (GetCubicMeters(CubicVolume.Text) != null)
                m_HouseData.CubicMeters = GetCubicMeters(CubicVolume.Text);
            else
                Device.BeginInvokeOnMainThread(() => { DisplayAlert("ReturnInvalid", "GetCubicMeters = null", "ok"); });

        }
        
        private Tuple<int, int, int> GetCubicMeters(string _inputString)
        {
            List<string> splitString = _inputString.Split(':').ToList();

            for (int i = 0; i < splitString.Count; i++)
            {
                if (splitString[i] == ":")
                    splitString.RemoveAt(i);
            }
            
            if (splitString.Count != 3)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Not the correct amount of inputs", "input 3 values like this x:x:x", "ok");
                });
                return null;
            }

            Tuple<int, int, int> returnValue = new Tuple<int, int, int>
            (
                ParseIntValue(splitString[0], "first value is invalid"),
                ParseIntValue(splitString[1], "second value is invalid"),
                ParseIntValue(splitString[2], "third value is invalid")
            );
            return returnValue;
        }

        private int ParseIntValue(string _input, string _title)
        {
            int output;

            if (!int.TryParse(_input, out output))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert(_title, "Input invalid, please input number", "ok");
                });
            }

            return output;
        }
    }
}