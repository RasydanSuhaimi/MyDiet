using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;

namespace MyDiet
{
    internal class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://mydiet-5a857-default-rtdb.asia-southeast1.firebasedatabase.app/");

        public async Task AddRecord(string d, int c)
        {
            await firebase
                .Child("CalorieRecords")
                .PostAsync(new CalorieRecord() { Date = d, TotalCalorie = c });
        }

        public async Task<CalorieRecord> GetLatestCalorieRecord()
        {
            var record = (await firebase
            .Child("CalorieRecords")
            .OrderByKey()
            .LimitToLast(1)
            .OnceAsync<CalorieRecord>()).FirstOrDefault();

            return record?.Object;
        }

        public async Task SaveFood(string f, int c, string meal, DateTime dt )
        {
            await firebase
                .Child("SaveFood")
                .PostAsync(new SaveFood() { Food = f, CalorieCount = c, Meal = meal, DateRecorded = dt});
        }

        public async Task<List<SaveFood>> GetAllFoodRecord(string foodType = null, DateTime? currentDate = null)
        {
            var allRecords = (await firebase
                .Child("SaveFood")
                .OnceAsync<SaveFood>())
                .Select(item => new SaveFood
                {
                    Food = item.Object.Food,
                    CalorieCount = item.Object.CalorieCount,
                    Meal = item.Object.Meal,
                    DateRecorded = Convert.ToDateTime(item.Object.DateRecorded), // Convert to DateTime
                });

            if (!string.IsNullOrEmpty(foodType))
            {
                // Filter records based on foodType
                allRecords = allRecords.Where(record => record.Meal.ToLower() == foodType.ToLower());
            }

            if (currentDate.HasValue)
            {
                // Filter records based on currentDate
                allRecords = allRecords.Where(record => record.DateRecorded.Date == currentDate.Value.Date);
            }

            return allRecords.ToList();
        }


        //public async Task<int> GetTotalCalorieCount()
        //{
        //    var CalorieCount = await firebase
        //    .Child("SaveFood")
        //        .OnceAsync<SaveFood>(); // Assuming Food is the class representing items in the "SaveFood" table

        //    int sumCalorieCount = CalorieCount.Sum(CalorieCount => CalorieCount.Object.CalorieCount); // Assuming CalorieCount is the property representing calorie count

        //    return sumCalorieCount;
        //}

        public async Task<int> GetTotalCalorieCountForCurrentDate()
        {
            DateTime currentDate = DateTime.Today;

            var foodItems = await firebase
                .Child("SaveFood")
                .OnceAsync<SaveFood>();

            // Filter and sum the calorie count for the current date
            int sumCalorieCount = foodItems
                .Where(item => item.Object.DateRecorded.Date == currentDate.Date)
                .Sum(item => item.Object.CalorieCount);

            return sumCalorieCount;
        }

        public async Task DeleteFoodRecord()
        {
            await firebase
                .Child("SaveFood")
                .DeleteAsync();
        }
    }
}