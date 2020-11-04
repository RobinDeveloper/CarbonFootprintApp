using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarbonFootprint.utilities;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarbonFootprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingScreen : ContentPage
    {
        private UserData m_UserData = new UserData();
        private Car m_CarData = new Car();
        private House m_HouseData = new House();

        private string m_FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userdata.json");

        private Dictionary<string, Car.CarType> m_NameToCarType = new Dictionary<string, Car.CarType>
        {
            {"Hybrid", Car.CarType.Hybrid},
            {"Gas", Car.CarType.Gas},
            {"Electric", Car.CarType.Electric}
        };

        public SettingScreen()
        {
            File.Create(m_FileName).Close();
            InitializeComponent();

            foreach (string typeName in m_NameToCarType.Keys)
            {
                TypePicker.Items.Add(typeName);
            }
        }

        //TODO: add check when field is empty to keep default
        private void OnSubmit(object _sender, EventArgs _e)
        {
            m_UserData.Name = NameField.Text;

            m_CarData.Name = CarName.Text;
            m_CarData.Age = ParseIntValue(CarAge.Text, "Invalid car age");
            m_CarData.BuildYear = ParseIntValue(Carbuild.Text, "Invalid build year");
            m_CarData.GasMilage = ParseIntValue(Carbuild.Text, "Invalid milage input");
            Enum.TryParse(TypePicker.SelectedItem.ToString(), out m_CarData.CarEnergyType);

            
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
            
            m_UserData.Car = m_CarData;
            m_UserData.House = m_HouseData;
            UploadToJson();
        }

        private void UploadToJson()
        {
            string output = JsonConvert.SerializeObject(m_UserData);
            File.WriteAllText(m_FileName, String.Empty);
            File.WriteAllText(m_FileName, output);
            SettingScreenText.Text = "Uploaded to json \n" + m_FileName;
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