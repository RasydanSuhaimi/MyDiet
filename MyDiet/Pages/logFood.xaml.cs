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
            string formattedDate = currentDate.ToString("dd/MM/yyyy");

            displaysnack.ItemsSource = await firebaseHelper.GetAllFoodRecord("Snack", formattedDate);
            displaybreakfast.ItemsSource = await firebaseHelper.GetAllFoodRecord("Breakfast", formattedDate);
            displaylunch.ItemsSource = await firebaseHelper.GetAllFoodRecord("Lunch", formattedDate);
            displaydinner.ItemsSource = await firebaseHelper.GetAllFoodRecord("Dinner", formattedDate);

            var calorie = await firebaseHelper.GetTotalCalorieCountForCurrentDate();
            calorieConsume.Text = calorie.ToString();

            var latestRecord = await firebaseHelper.GetLatestCalorieRecord();
            if (latestRecord != null)
            {
                double remainingCalories = latestRecord.TotalCalorie - calorie;

                displayCalorieNeed.Text = $"{latestRecord.TotalCalorie}";
                displayRemaining.Text = $"{remainingCalories.ToString()}";

                // Change the text color based on the value of remainingCalories
                if (remainingCalories < 0)
                {
                    displayRemaining.TextColor = Colors.Red;
                    warningLabel.Text = "You have exceeded your calorie goal!";
                    warningLabel.IsVisible = true;
                }
                else
                {
                    displayRemaining.TextColor = Colors.Black; // or another color of your choice
                    warningLabel.IsVisible = false;
                }
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
