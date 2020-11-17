using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
            switch (FurnitureTypepicker.SelectedItem)
            {
                case "Wooden Table":
                    AddScoreToUserData(GetValue("Wooden Table", m_FurnitureItems), PositivityRating.Negative);
                    break;
                case "Wooden Chair":
                    AddScoreToUserData(GetValue("Wooden Chair", m_FurnitureItems), PositivityRating.Negative);
                    break;
                case "Steel Table":
                    AddScoreToUserData(GetValue("Steel Table", m_FurnitureItems), PositivityRating.Medium);
                    break;
                case "Steel Chair":
                    AddScoreToUserData(GetValue("Steel Chair", m_FurnitureItems), PositivityRating.Medium);
                    break;
                case "Bamboo Table":
                    AddScoreToUserData(GetValue("Bamboo Table", m_FurnitureItems), PositivityRating.Positive);
                    break;
                case "Bamboo Chair":
                    AddScoreToUserData(GetValue("Bamboo Chair", m_FurnitureItems), PositivityRating.Positive);
                    break;
                default:
                    break;
            }
        }
        
        private void HandleTechnology()
        {
            switch (TechnologyTypepicker.SelectedItem)
            {
                case "Phone":
                    AddScoreToUserData(GetValue("Phone", m_TechnologyItems), PositivityRating.Negative);
                    break;
                case "Laptop":
                    AddScoreToUserData(GetValue("Laptop", m_TechnologyItems), PositivityRating.Negative);
                    break;
                case "Airpods":
                    AddScoreToUserData(GetValue("Airpods", m_TechnologyItems), PositivityRating.Negative);
                    break;
                case "Charger":
                    AddScoreToUserData(GetValue("Charger", m_TechnologyItems), PositivityRating.Negative);
                    break;
                default:
                    break;
            }
        }

        private void HandleClothing()
        {
            switch (ClothingTypepicker.SelectedItem)
            {
                case "Shirt":
                    AddScoreToUserData(GetValue("Shirt", m_ClothingItems), PositivityRating.Medium);
                    break;
                case "Pants":
                    AddScoreToUserData(GetValue("Pants", m_ClothingItems), PositivityRating.Positive);
                    break;
                case "Hoodie":
                    AddScoreToUserData(GetValue("Hoodie", m_ClothingItems), PositivityRating.Medium);
                    break;
                case "Socks":
                    AddScoreToUserData(GetValue("Socks", m_ClothingItems), PositivityRating.Negative);
                    break;
                case "Jeans":
                    AddScoreToUserData(GetValue("Jeans", m_ClothingItems), PositivityRating.Medium);
                    break;
                default:
                    break;
            }
        }

        private float GetValue(string _input, Dictionary<string, float> _getFrom)
        {
            if (_getFrom.TryGetValue(_input, out float value))
                return value;
            
            
            DisplayAlert("Error", "unexpected error", "Ok");
            return 0;
        }

        private void AddScoreToUserData(float _score, PositivityRating _rating)
        {
            switch (_rating)
            {
                case PositivityRating.Positive:
                    m_UserData.PMNUDayScore.Positive += _score;
                    m_UserData.PMNUWeekScore.Positive +=  _score;
                    m_UserData.PMNUMonthScore.Positive += _score;
                    m_UserData.PMNUYearScore.Positive +=  _score;
                    m_UserData.ProdcutScore.Positive += _score;
                    break;
                case PositivityRating.Medium:
                    m_UserData.PMNUDayScore.Medium += _score;
                    m_UserData.PMNUWeekScore.Medium +=  _score;
                    m_UserData.PMNUMonthScore.Medium +=  _score;
                    m_UserData.PMNUYearScore.Medium +=  _score;
                    m_UserData.ProdcutScore.Medium += _score;
                    break;
                case PositivityRating.Negative:
                    m_UserData.PMNUDayScore.Negative += _score;
                    m_UserData.PMNUWeekScore.Negative +=  _score;
                    m_UserData.PMNUMonthScore.Negative +=  _score;
                    m_UserData.PMNUYearScore.Negative +=  _score;
                    m_UserData.ProdcutScore.Negative += _score;
                    break;
                case PositivityRating.Unkown:
                    m_UserData.PMNUDayScore.Unkowm += _score;
                    m_UserData.PMNUWeekScore.Unkowm +=  _score;
                    m_UserData.PMNUMonthScore.Unkowm +=  _score;
                    m_UserData.PMNUYearScore.Unkowm += _score;
                    m_UserData.ProdcutScore.Unkowm += _score;
                    break;
                default:
                    break;
            }

            UploadData();
        }

        private void UploadData()
        {
            Jsonhandler.Instance.UploadJson("userdata.json", m_UserData);
            Chart.BindingContext = new ViewmodelProduct();
        }
    }
    
    public class ViewmodelProduct
    {
        public ObservableCollection<PieDataProduct> Data { get; set; }
        
        private UserData m_UserData;

        public ViewmodelProduct()
        {
            m_UserData = Jsonhandler.Instance.RequestObject<UserData>("userdata.json");
            
            Data = new ObservableCollection<PieDataProduct>()
            {
                new PieDataProduct(m_UserData.ProdcutScore.Positive, "Positive", Color.Green),
                new PieDataProduct(m_UserData.ProdcutScore.Medium, "Medium", Color.Yellow),
                new PieDataProduct(m_UserData.ProdcutScore.Negative, "Negative", Color.Red),
                new PieDataProduct(m_UserData.ProdcutScore.Unkowm, "Unkown", Color.Gray)
            };
        }
    }

    public class PieDataProduct
    {
        public double Value { get; set; }
        public string Rating { get; set; }
        public Color Color { get; set; }

        public PieDataProduct(double _value, string _rating, Color _color)
        {
            Value = _value;
            Rating = _rating;
            Color = _color;
        }
    }
}