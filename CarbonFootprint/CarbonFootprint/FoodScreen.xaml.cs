using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarbonFootprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodScreen : ContentPage
    {

        public FoodScreen()
        {
            InitializeComponent();
        }

        async void NewValue(object _sender, ValueChangedEventArgs _e)
        {
            Stepper stepper = ((Stepper)_sender);
            double newValue = _e.NewValue;
            double oldValue = _e.OldValue;
            if (oldValue < newValue)
            {
                var actionSheet = await DisplayActionSheet("How did you make it?", null, null, "Furnice", "Microwave/Oven", "Bought ready");
                
                switch (stepper.ClassId)
                {
                    case "VeganStepper":
                        VeganAmount.Text = newValue.ToString();
                        break;
                    case "VegetarianStepper":
                        VegetarianAmount.Text = newValue.ToString();
                        break;
                    case "PescotarianStepper":
                        PescotarianAmount.Text = newValue.ToString();
                        break;
                    case "LowMeatStepper":
                        LowMeatAmount.Text = newValue.ToString();
                        break;
                    case "MediumMeatStepper":
                        MediumMeatAmount.Text = newValue.ToString();
                        break;
                    case "HighMeatStepper":
                        HighMeatAmount.Text = newValue.ToString();
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
                        break;
                    case "VegetarianStepper":
                        VegetarianAmount.Text = newValue.ToString();
                        break;
                    case "PescotarianStepper":
                        PescotarianAmount.Text = newValue.ToString();
                        break;
                    case "LowMeatStepper":
                        LowMeatAmount.Text = newValue.ToString();
                        break;
                    case "MediumMeatStepper":
                        MediumMeatAmount.Text = newValue.ToString();
                        break;
                    case "HighMeatStepper":
                        HighMeatAmount.Text = newValue.ToString();
                        break;
                }
            }
        }
    }
}