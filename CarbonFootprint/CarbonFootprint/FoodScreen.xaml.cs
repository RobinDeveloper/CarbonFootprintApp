using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarbonFootprint.DataCollection;
using CarbonFootprint.utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarbonFootprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodScreen : ContentPage
    {
        double m_TotalCarbonFood;
        double m_CookingCarbon;
        double m_MealCarbon;
        double m_CookingMealCarbon;
        double[] FoodArray = new double[6];
        private GeneralAppliances m_GeneralAppliances;
        private UserData m_UserData;

        public FoodScreen()
        {
            InitializeComponent();
           

            if (Jsonhandler.Instance.CheckIfFileExists("userdata.json"))
            {
                m_UserData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");
            }
            else
            {
                DisplayAlert("No userdata data found ", "Fill in the setting screen", "Ok");
            }

            if (Jsonhandler.Instance.CheckIfFileExists("appliences.json"))
            {
                m_GeneralAppliances = Jsonhandler.Instance.RequestObject<GeneralAppliances>("appliences.json");
            }
            else
            {
                DisplayAlert("No appliance data found ","Fill in the regular expanses screen","Ok");
            }
        }

        async void NewValue(object _sender, ValueChangedEventArgs _e)
        {
            Stepper stepper = ((Stepper)_sender);
            double newValue = _e.NewValue;
            double oldValue = _e.OldValue;
            if (oldValue < newValue)
            {
                var actionSheet = await DisplayActionSheet("How did you make it?", null, null, "Furnice", "Microwave/Oven", "Bought ready", "Cold meal");
                
                switch (actionSheet)
                {
                    case "Furnice":
                        m_CookingCarbon = m_GeneralAppliances.CookingValue;
                        break;
                    case "Microwave/Oven":
                        m_CookingCarbon = 0.321;
                        break;
                    case "Bought ready":
                        m_CookingCarbon = 0.543;
                        break;
                    case "Cold meal":
                        m_CookingCarbon = 0;
                        break;
                }

                switch (stepper.ClassId)
                {
                    case "VeganStepper":
                        VeganAmount.Text = newValue.ToString();
                        m_MealCarbon = 0.5;
                        m_CookingMealCarbon = m_CookingCarbon + m_MealCarbon;
                        FoodArray[0] = m_CookingMealCarbon;
                        TotalSum(m_CookingMealCarbon);
                        break;
                    case "VegetarianStepper":
                        VegetarianAmount.Text = newValue.ToString();
                        m_MealCarbon = 0.567;
                        m_CookingMealCarbon = m_CookingCarbon + m_MealCarbon;
                        FoodArray[1] = m_CookingMealCarbon;
                        TotalSum(m_CookingMealCarbon);
                        break;
                    case "PescotarianStepper":
                        PescotarianAmount.Text = newValue.ToString();
                        m_MealCarbon = 0.6;
                        m_CookingMealCarbon = m_CookingCarbon + m_MealCarbon;
                        FoodArray[2] = m_CookingMealCarbon;
                        TotalSum(m_CookingMealCarbon);
                        break;
                    case "LowMeatStepper":
                        LowMeatAmount.Text = newValue.ToString();
                        m_MealCarbon = 0.633;
                        m_CookingMealCarbon = m_CookingCarbon + m_MealCarbon;
                        FoodArray[3] = m_CookingMealCarbon;
                        TotalSum(m_CookingMealCarbon);
                        break;
                    case "MediumMeatStepper":
                        MediumMeatAmount.Text = newValue.ToString();
                        m_MealCarbon = 0.833;
                        m_CookingMealCarbon = m_CookingCarbon + m_MealCarbon;
                        FoodArray[4] = m_CookingMealCarbon;
                        TotalSum(m_CookingMealCarbon);
                        break;
                    case "HighMeatStepper":
                        HighMeatAmount.Text = newValue.ToString();
                        m_MealCarbon = 1.1;
                        m_CookingMealCarbon = m_CookingCarbon + m_MealCarbon;
                        FoodArray[5] = m_CookingMealCarbon;
                        TotalSum(m_CookingMealCarbon);
                        break;
                }                    
            }

            if(oldValue > newValue)
            {
                await DisplayAlert("Alert", "Your meal has succesfully been removed.", "OK");
                switch (stepper.ClassId)
                {
                    case "VeganStepper":
                        VeganAmount.Text = newValue.ToString();
                        m_CookingMealCarbon = FoodArray[0];
                        MinusTotal(m_CookingMealCarbon);
                        break;
                    case "VegetarianStepper":
                        VegetarianAmount.Text = newValue.ToString();
                        m_CookingMealCarbon = FoodArray[1];
                        MinusTotal(m_CookingMealCarbon);
                        break;
                    case "PescotarianStepper":
                        PescotarianAmount.Text = newValue.ToString();
                        m_CookingMealCarbon = FoodArray[2];
                        MinusTotal(m_CookingMealCarbon);
                        break;
                    case "LowMeatStepper":
                        LowMeatAmount.Text = newValue.ToString();
                        m_CookingMealCarbon = FoodArray[3];
                        MinusTotal(m_CookingMealCarbon);
                        break;
                    case "MediumMeatStepper":
                        MediumMeatAmount.Text = newValue.ToString();
                        m_CookingMealCarbon = FoodArray[4];
                        MinusTotal(m_CookingMealCarbon);
                        break;
                    case "HighMeatStepper":
                        HighMeatAmount.Text = newValue.ToString();
                        m_CookingMealCarbon = FoodArray[5];
                        MinusTotal(m_CookingMealCarbon);
                        break;
                }
            }
        }

        private void AddData()
        {
            m_UserData.FoodScore.Unkowm += m_TotalCarbonFood;
            Jsonhandler.Instance.UploadJson("userdata.json", m_UserData);
            Chart.BindingContext = new ViewmodelFood();
        }

        private void TotalSum(double plusValue)
        {
            m_TotalCarbonFood = m_TotalCarbonFood + plusValue;
            AddData();
        }
        private void MinusTotal(double minusValue)
        {
            m_TotalCarbonFood = m_TotalCarbonFood - minusValue;
            AddData();
        }
    }
    
    public class ViewmodelFood
    {
        public ObservableCollection<PieDataProduct> Data { get; set; }
        
        private UserData m_UserData;

        public ViewmodelFood()
        {
            m_UserData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");
            
            Data = new ObservableCollection<PieDataProduct>()
            {
                new PieDataProduct(m_UserData.FoodScore.Positive, "Positive", Color.Green),
                new PieDataProduct(m_UserData.FoodScore.Medium, "Medium", Color.Yellow),
                new PieDataProduct(m_UserData.FoodScore.Negative, "Negative", Color.Red),
                new PieDataProduct(m_UserData.FoodScore.Unkowm, "Unkown", Color.Gray)
            };
        }
    }
}