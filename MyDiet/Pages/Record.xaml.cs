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
    }

}