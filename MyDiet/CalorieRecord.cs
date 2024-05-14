using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiet
{
    public class CalorieRecord
    {
        //public static int[] Text { get; internal set; }
        public string Date { get; set; }
        public int TotalCalorie { get; set; }
        public string? Text { get; internal set; }

        public static implicit operator Label(CalorieRecord v)
        {
            throw new NotImplementedException();
        }
    }
}
