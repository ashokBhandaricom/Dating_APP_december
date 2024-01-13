using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static  class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob){
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year -dob.Year; 
           
            return age ; 
        }
    }
}