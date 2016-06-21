using System;
using System.Collections.Generic;
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

        public static DateTime startDate = new DateTime(2016,7,1);

        public static DateTime endDate = new DateTime(2016, 7, 31);

        public static int matchesPerTeamOnWeekend = -1; // if -1 then we don't care about this
 



    }
}