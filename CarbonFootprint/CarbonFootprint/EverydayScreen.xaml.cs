using CarbonFootprint.DataCollection;
using CarbonFootprint.utilities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarbonFootprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EveryDayScreen : ContentPage
    {
        double m_CarbonCooking;
        double m_CarbonPet;
        List<double> YourPets = new List<double>();
        double m_TotalCarbonPet;
        double m_CarbonHeating;
        double m_CarbonElectricity;
        double m_StepperValue = 1;

        private GeneralAppliances m_GeneralAppliencesData;

        public EveryDayScreen()
        {
            InitializeComponent();

           if (Jsonhandler.Instance.CheckIfFileExists("appliences.json"))
                m_GeneralAppliencesData = Jsonhandler.Instance.RequestObject<GeneralAppliances>("appliences.json");
           else
               m_GeneralAppliencesData = new GeneralAppliances();
        }

        private void OnNewValue(object _sender, ValueChangedEventArgs _e)
        {
            double value = ((Stepper)_sender).Value;
            m_StepperValue = ((Stepper)_sender).Value;
            HousematesInput.Text = Convert.ToString(value);
        }

        private void AddCookingType(object _sender, EventArgs _e)
        {

            if (TypeOfGasCook.SelectedItem.Equals("Gas"))
            {
                m_CarbonCooking = 0.191;
                AddData();
            }
            if (TypeOfGasCook.SelectedItem.Equals("Induction"))
            {
                m_CarbonCooking = 0;
                AddData();
            }
        }

        private void AddPetType(object _sender, EventArgs _e)
        {
            switch (TypeOfPet.SelectedItem)
            {
                case "Cat":
                    m_CarbonPet = 0.126;
                    YourPets.Add(m_CarbonPet);
                    SumPets();
                    AddData();
                    break;
                case "Dog":
                    m_CarbonPet = 0.327;
                    YourPets.Add(m_CarbonPet);
                    SumPets();
                    AddData();
                    break;
                case "Rabbit":
                    m_CarbonPet = 0.061;
                    YourPets.Add(m_CarbonPet);
                    SumPets();
                    AddData();
                    break;
                case "Fish":
                    m_CarbonPet = 0.001;
                    YourPets.Add(m_CarbonPet);
                    SumPets();
                    AddData();
                    break;
                case "Bird":
                    m_CarbonPet = 0.011;
                    YourPets.Add(m_CarbonPet);
                    SumPets();
                    AddData();
                    break;
                case "Horse":
                    m_CarbonPet = 1.906;
                    YourPets.Add(m_CarbonPet);
                    SumPets();
                    AddData();
                    break;
                case "Other":
                    DisplayPromptAsync("Put an other pet", "Type your pet");
                    m_CarbonPet = 0.2265;
                    YourPets.Add(m_CarbonPet);
                    SumPets();
                    AddData();
                    break;
            }
        }

        private void RemovePetType(object _sender, EventArgs _e)
        {
            switch (TypeOfPet.SelectedItem)
            {
                case "Cat":
                    m_CarbonPet = 0.126;
                    YourPets.Remove(0.126);
                    MinusSumPets();
                    AddData();
                    break;
                case "Dog":
                    m_CarbonPet = 0.327;
                    YourPets.Remove(0.327);
                    MinusSumPets();
                    AddData();
                    break;
                case "Rabbit":
                    m_CarbonPet = 0.061;
                    YourPets.Remove(0.061);
                    MinusSumPets();
                    AddData();
                    break;
                case "Fish":
                    m_CarbonPet = 0.001;
                    YourPets.Remove(0.001);
                    MinusSumPets();
                    AddData();
                    break;
                case "Bird":
                    m_CarbonPet = 0.011;
                    YourPets.Remove(0.011);
                    MinusSumPets();
                    AddData();
                    break;
                case "Horse":
                    m_CarbonPet = 1.906;
                    YourPets.Remove(1.996);
                    MinusSumPets();
                    AddData();
                    break;
                case "Other":
                    DisplayPromptAsync("Put an other pet", "Type your pet");
                    m_CarbonPet = 0.2265;
                    YourPets.Remove(0.2265);
                    MinusSumPets();
                    AddData();
                    break;
            }
        }

        private void AddHeatingType(object _sender, EventArgs _e)
        {
            switch (HeatType.SelectedItem)
            {
                case "Wood":
                    m_CarbonHeating = 1.6;
                    AddData();
                    break;
                case "Electricity":
                    m_CarbonHeating = 0;
                    AddData();
                    break;
                case "Solar-energy":
                    m_CarbonHeating = 0;
                    AddData();
                    break;
                case "Gas":
                    m_CarbonHeating = 0.056;
                    AddData();
                    break;
            }
        }

        private void AddElectricityUse(object _sender, EventArgs _e)
        {
            string _text = EverageDay.Text;
           if(!double.TryParse(_text, out double ElectricityUsage))
            {
                DisplayAlert("Warning", "Put a valid number", "OK");
            }
           else
            {
                ElectricityUsage = Convert.ToDouble(EverageDay.Text);
                double CarbonElectric = 5.178;
                m_CarbonElectricity = ElectricityUsage / m_StepperValue * CarbonElectric;
                AddData();
            }
        }

        private void AddData()
        {
            m_GeneralAppliencesData.CookingValue = m_CarbonCooking;
            m_GeneralAppliencesData.PetValue = m_TotalCarbonPet;
            m_GeneralAppliencesData.HeatingValue = m_CarbonHeating;
            m_GeneralAppliencesData.Electricity = m_CarbonElectricity;

           Jsonhandler.Instance.UploadJson("appliences.json", m_GeneralAppliencesData);
        }

        private double SumPets()
        {
            m_TotalCarbonPet = 0;
            int m_PetTotal = YourPets.Count;
            for (int i =0; i < m_PetTotal; i++)
            {
                m_TotalCarbonPet = m_TotalCarbonPet + YourPets.ElementAt(i);
            }
            return m_TotalCarbonPet;
        }

        private double MinusSumPets()
        {
            if (m_TotalCarbonPet == 0)
            {
                DisplayAlert("Warning", "Add a pet first", "OK");
                return m_TotalCarbonPet = 0;
            }
            else
            {
                m_TotalCarbonPet = m_TotalCarbonPet - m_CarbonPet;
                return m_TotalCarbonPet;
            }
        }
    }
}
