namespace MyDiet.Pages;

public partial class Record : ContentPage
{
    FirebaseHelper firebaseHelper = new FirebaseHelper();
    public Record()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        displayRecord.ItemsSource = await firebaseHelper.GetAllFoodRecord();

        var calorie = await firebaseHelper.GetTotalCalorieCountForCurrentDate();
        calorieConsume.Text = calorie.ToString();

    }

    private async void DeleteFoodRecordAsync(object sender, EventArgs e)
    {
        bool confirmed = await DisplayAlert("Confirmation", "Are you sure you want to delete this record?", "Yes", "No");

        if (confirmed)
        {
            // Perform deletion if user confirms
            await firebaseHelper.DeleteFoodRecord();
            // Optionally, you can refresh the record list after deletion
            displayRecord.ItemsSource = await firebaseHelper.GetAllFoodRecord();
        }
    }

    async void onDatePickerSelected(object sender, DateChangedEventArgs e)
    {
        try
        {
            var selectedDate = e.NewDate.ToString("dd/MM/yyyy");
            displayRecord.ItemsSource = await firebaseHelper.GetSelectedFoodRecord(selectedDate);

        }
        catch (Exception ex)
        {
            // Handle the exception (e.g., log it, show an error message)
            Console.WriteLine($"Error in onDatePickerSelected: {ex.Message}");
        }
    }





}