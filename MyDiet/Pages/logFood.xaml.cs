using Syncfusion.Maui.Core.Carousel;
using Microsoft.Maui.Controls;

namespace MyDiet.Pages
{
    public partial class logFood : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            DateTime currentDate = DateTime.Today;
            displaysnack.ItemsSource = await firebaseHelper.GetAllFoodRecord("Snack", currentDate);
            displaybreakfast.ItemsSource = await firebaseHelper.GetAllFoodRecord("Breakfast", currentDate);
            displaylunch.ItemsSource = await firebaseHelper.GetAllFoodRecord("Lunch", currentDate);
            displaydinner.ItemsSource = await firebaseHelper.GetAllFoodRecord("Dinner", currentDate);


            var calorie = await firebaseHelper.GetTotalCalorieCountForCurrentDate();
            calorieConsume.Text = calorie.ToString();

            var latestRecord = await firebaseHelper.GetLatestCalorieRecord();
            if (latestRecord != null)
            {
                // Assuming displayCalorieNeed is a Label
                double remainingCalories = latestRecord.TotalCalorie - calorie;

                displayCalorieNeed.Text = $"{latestRecord.TotalCalorie}";

                displayRemaining.Text = $"{remainingCalories.ToString()}";
            }

        }

        public logFood()
        {
            InitializeComponent();
        }

        private async void AddSnackButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the AddSnackPage
            await Navigation.PushAsync(new AddSnackPage());
        }

        private async void AddBreakfastButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the AddBreakfastPage
            await Navigation.PushAsync(new AddBreakfastPage());
        }

        private async void AddLunchButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the AddSnackPage
            await Navigation.PushAsync(new AddLunchPage());
        }
        private async void AddDinnerButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the AddSnackPage
            await Navigation.PushAsync(new AddDinnerPage());
        }
    }
}
