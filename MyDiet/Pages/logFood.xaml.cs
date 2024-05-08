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

            // Get the latest calorie record
            var latestRecord = await firebaseHelper.GetLatestCalorieRecord();
            displayRecord.ItemsSource = new[] { latestRecord };

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
    }
}
