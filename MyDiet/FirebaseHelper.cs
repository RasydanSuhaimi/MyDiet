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

        public async Task SaveFood(string s, int c, string meal, string dt )
        {
            await firebase
                .Child("SaveFood")
                .PostAsync(new SaveFood() { FoodPicker = s, CalorieCount = c, Meal = meal, DateRecorded = dt});
        }

        public async Task<List<SaveFood>> GetAllFoodRecord()
        {
            return (await firebase
            .Child("SaveFood")
            .OnceAsync<SaveFood>()).Select(item => new SaveFood
            {
                FoodPicker = item.Object.FoodPicker,
                CalorieCount = item.Object.CalorieCount,
                Meal = item.Object.Meal,
                DateRecorded = item.Object.DateRecorded,
            }).ToList();
        }       
    }
}