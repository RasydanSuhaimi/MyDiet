using System.ComponentModel;
using Microsoft.Maui.Controls;

namespace MyDiet.Pages
{
    public partial class CalorieCalculator : ContentPage, INotifyPropertyChanged
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        double _progress;
        public double Progress
        {
            get { return _progress; }
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged(nameof(Progress));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CalorieCalculator()
        {
            InitializeComponent();
            BindingContext = this;
        }

        void OnCalculateCalorie(object sender, EventArgs e)
        {
            var weight = 0.0;
            var height = 0;
            var age = 0;
            var gender = "male"; // default to male
            var TotalCalorie = 0.0;

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
                    TotalCalorie = 66.47 + (13.75 * weight) + (5.003 * height) - (6.755 * age);
                }
                else if (gender == "female")
                {
                    TotalCalorie = 655.1 + (9.563 * weight) + (1.850 * height) - (4.676 * age);
                }

                Progress = TotalCalorie;
                outputResult.FormattedText = new FormattedString();
                outputResult.FormattedText.Spans.Add(new Span { Text = string.Format("Your calorie intake is "), FontAttributes = FontAttributes.None });
                outputResult.FormattedText.Spans.Add(new Span { Text = string.Format("{0:#} Cal", (int)TotalCalorie), FontAttributes = FontAttributes.Bold });
                outputResult.FormattedText.Spans.Add(new Span { Text = string.Format(" per day"), FontAttributes = FontAttributes.None });

                // Hide the keyboard
                inputAge?.Unfocus();
                radioMale?.Unfocus();
                radioFemale?.Unfocus();

            }
            else
            {
                outputResult.Text = "Please enter a valid value";
            }
        }

        void onDatePickerSelected(object sender, DateChangedEventArgs e) 
        { 
            var selectedDate = e.NewDate.ToString(); 
        }

        async void OnSaveRecord(object sender, EventArgs e)
        {

            var formattedText = outputResult.FormattedText;
            var span = formattedText.Spans.FirstOrDefault(s => s.FontAttributes == FontAttributes.Bold);
            if (span == null || !int.TryParse(span.Text.Replace(" Cal", ""), out int TotalCalorie))
            {
                await DisplayAlert("Error", "Please enter a valid integer value", "OK");
                return;
            }

            var selectdate = selectDate.Date.ToString("dd/MM/yyyy");
            await firebaseHelper.AddRecord(selectdate, TotalCalorie);
            await DisplayAlert("Record Saved", "Calorie Record has been saved", "OK");
            //Clear Input
            //inputWorkout.SelectedItem = null;
            //inputCounter.Text = null;
        }
    }
}
