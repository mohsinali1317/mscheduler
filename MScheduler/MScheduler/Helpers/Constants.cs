using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MScheduler.Helpers
{
    public class Constants
    {
        public static List<string> grounds = new List<string>{
            "Ekerbeg1",
            "Ekerbeg1",
            "Rommen",
            "Stubermyra",
            "Drammen",
            "Bærum"
        };

        public static List<DateTime> exceptionDates = new List<DateTime>{
           new DateTime(2016,7,4)
        };

        public static List<TimeSpan> allowedTimes = new List<TimeSpan>{
           new TimeSpan(9,15,0),
           new TimeSpan(3,15,0)
        };

        public static DateTime startDate = new DateTime(2016,5,1);

        public static DateTime endDate = new DateTime(2016, 9, 30);

        public static int matchesPerTeamOnWeekend = -1; // if -1 then we don't care about this


        public static string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static List<string> elite = new List<string>();

        public static List<string> first = new List<string>();



    }
}