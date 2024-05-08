namespace MyDiet
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
        }

        void OnCalculateCalorie(object sender, EventArgs e)
        {
            var weight = 0.0;
            var height = 0;
            var age = 0;
            var gender = "male"; // default to male
            var calorie = 0.0;

            if (Double.TryParse(inputWeight.Text, out weight) && int.TryParse(inputHeight.Text, out height) && int.TryParse(inputAge.Text, out age))
            {
                if (radioMale.IsChecked)
                {
                    gender = "male";
                }
                else if (radioFemale.IsChecked)
                {
                    gender = "female";
                }

                if (gender == "male")
                {
                    calorie = 66.47 + (13.75 * weight) + (5.003 * height) - (6.755 * age);
                }
                else if (gender == "female")
                {
                    calorie = 655.1 + (9.563 * weight) + (1.850 * height) - (4.676 * age);
                }

                outputResult.Text = string.Format("{0:##.00}", calorie);
            }
            else
            {
                outputResult.Text = "Please enter a valid value";
            }
        }

    }

    

}
