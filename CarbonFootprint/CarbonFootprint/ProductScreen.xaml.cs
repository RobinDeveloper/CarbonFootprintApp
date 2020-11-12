using System;
using System.Collections.Generic;
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
    public partial class ProductScreen : ContentPage
    {
        private Dictionary<string, float> m_TechnologyItems;
        private Dictionary<string, float> m_ClothingItems;
        private Dictionary<string, float> m_FurnitureItems;

        private UserData m_UserData;
        
        public ProductScreen()
        {
            InitializeComponent();

            m_UserData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");
            
            InitializeDictionaries();
        }

        private void InitializeDictionaries()
        {
            m_TechnologyItems = new Dictionary<string, float>
            {
                { "Phone", 60 },
                { "Laptop", 320},
                { "Airpods", 5},
                { "Charger", 1},
            };
            m_FurnitureItems = new Dictionary<string, float>
            {
                { "Wooden Table", 5 },
                { "Wooden Chair", 4 },
                { "Steel Chair", 0.91f},
                { "Steel Table", 1.91f},
                { "Bamboo Chair", 2},
                { "Bamboo Table", 4}
            };
            m_ClothingItems = new Dictionary<string, float> //This is based on bad practices not the good initiatives
            {                                               //Gucci is carbon neutral fjallraven does a lot of great thing
                { "Shirt", 2.35f },                         //So do more High fashion brands and the northface
                { "Pants", 1.98f },                         //Burberry and LV are just as bad as fast fashion
                { "Hoodie", 3},
                { "Socks", 3},
                { "Jeans", 1.98f}
            };
            
            foreach (string typeName in m_TechnologyItems.Keys)
            {
                TechnologyTypepicker.Items.Add(typeName);
            }
            
            foreach (string typeName in m_FurnitureItems.Keys)
            {
                FurnitureTypepicker.Items.Add(typeName);
            }
            
            foreach (string typeName in m_ClothingItems.Keys)
            {
                ClothingTypepicker.Items.Add(typeName);
            }
        }

        private void SubmitButtonClicked(object _sender, EventArgs _e)
        {
            Button pressedButton = ((Button) _sender);
            string classIDButton = pressedButton.ClassId;
            
            switch (classIDButton)
            {
                case "FurnitureButton":
                    HandleFurniture();
                    break;
                case "TechnologyButton":
                    HandleTechnology();
                    break;
                case "ClothingButton":
                    HandleClothing();
                    break;
            }
        }

        private void HandleFurniture()
        {
            
        }

        private void HandleTechnology()
        {
            
        }

        private void HandleClothing()
        {
            
        }

        private void AddScoreToUserData(float _score)
        {
            
        }
    }
}