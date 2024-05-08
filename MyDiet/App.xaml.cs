using MyDiet.Pages;
namespace MyDiet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        protected override void OnStart()
        {
            Shell.Current.GoToAsync(nameof(CalorieCalculator));
        }
    }
}
