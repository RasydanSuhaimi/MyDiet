using Syncfusion.Maui.Core.Carousel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiet.Pages
{
    public partial class AddSnackPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public AddSnackPage()
        {
            InitializeComponent();
            BindingContext = new AddFoodViewModel();
        }

        public class AddFoodViewModel
        {
            public List<string> FoodOptions { get; set; } = new List<string>
            {
                "Apple",
                "Banana",
                "Carrot",
                "Chips",
                "Cookie"
            };

            public string SelectedFood { get; set; }

            public int CalorieCount { get; set; }

        }

        async void SaveRecord(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string date = currentDate.ToString("d/M/yyyy");

            if (string.IsNullOrWhiteSpace(CalorieCount.Text))
            {
                await DisplayAlert("Error", "Please enter a calorie count", "OK");
                return;
            }

            if (!int.TryParse(CalorieCount.Text, out int calorie))
            {
                await DisplayAlert("Error", "Please enter a valid integer value", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(FoodPicker.SelectedItem.ToString()))
            {
                await DisplayAlert("Error", "Please select a snack", "OK");
                return;
            }
            string food = FoodPicker.SelectedItem.ToString(); 
            await firebaseHelper.SaveFood(food, calorie, "snack", date);
            // Update the total calorie count
            //Record.TotalCalorieCount += calorie;
            await DisplayAlert("Record Saved", "Record has been saved", "OK");

            await Navigation.PopAsync();
        }
    }
}