
namespace MyDiet.Pages;

public partial class AddBreakfastPage : ContentPage
{
    FirebaseHelper firebaseHelper = new FirebaseHelper();
    public AddBreakfastPage()
    {
		InitializeComponent();
        BindingContext = new AddFoodViewModel();
    }
    public class AddFoodViewModel
    {

        public string Food { get; set; }
        public string CalorieCount { get; set; }

    }

    async void SaveRecord(object sender, EventArgs e)
    {
        DateTime currentDate = DateTime.Now;
        string formattedDate = currentDate.ToString("dd/MM/yyyy");

        if (string.IsNullOrWhiteSpace(Food.Text))
        {
            await DisplayAlert("Error", "Please enter a snack", "OK");
            return;
        }

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

        await firebaseHelper.SaveFood(Food.Text, calorie, "Breakfast", formattedDate);

        await DisplayAlert("Record Saved", "Record has been saved", "OK");

        await Navigation.PopAsync();
    }
}