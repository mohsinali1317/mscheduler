using MScheduler.Helpers;
using MScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MScheduler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<string> elite = new List<string>
            {
                "star",
                "sentrum",
                "oslo"
            };

            List<string> first = new List<string>
            {
                "falken",
                "united",
                "city"
            };


            List<Match> matches = new List<Match>();



            int totalNumberOfMatchesInElite = (elite.Count() * (elite.Count() - 1));
            int totalNumberOfMatchesInFirst = (first.Count() * (first.Count() - 1));

            int totalNumberOfMatches = totalNumberOfMatchesInElite + totalNumberOfMatchesInFirst;

            int maximumMatchesPerTeamElite = (elite.Count() - 1) * 2;
            int maximumMatchesPerTeamFirst = (first.Count() - 1) * 2;

            int count = 0;

            Random rand = new Random();

            int ranTeam;

            string team;

            int ranTeam1;

            string otherTeam;

            while (count < totalNumberOfMatches)
            {
                int random = rand.Next(0, 2);

                int randomGround = rand.Next(0, 6);

                TimeSpan timeSpan = Constants.endDate - Constants.startDate;
                var randomTest = new Random();
                TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
                DateTime newDate = Constants.startDate + newSpan;


                TimeSpan time = Constants.allowedTimes.ElementAt(random);

                if (newDate.DayOfWeek.ToString().ToLower() != "saturday" && newDate.DayOfWeek.ToString().ToLower() != "sunday")
                    continue;

                String ground = Constants.grounds.ElementAt(randomGround).ToLower();

                if (matches.Where(i => i.ground.ToLower() == ground && i.date == newDate).Count() > 0)
                    continue;

                if (matches.Where(i => i.ground.ToLower() == ground && i.date == newDate && i.time == time).Count() > 0)
                    continue;

                switch (random)
                {

                    case 0: // elite division

                        // check if all matches have happened 

                        if (matches.Where(i => i.division == "elite").Count() == totalNumberOfMatchesInElite)
                            continue;

                        ranTeam = rand.Next(0, 3);

                        team = elite.ElementAt(ranTeam);

                        if (matches.Where(i => i.firstTeam == team || i.secondTeam == team).Count() >= maximumMatchesPerTeamElite) // maximum matches per team
                            continue;

                        ranTeam1 = rand.Next(0, 3);

                        if (ranTeam == ranTeam1)
                        {
                            while (ranTeam == ranTeam1)
                            {
                                ranTeam1 = rand.Next(0, 3);
                            }
                        }

                        otherTeam = elite.ElementAt(ranTeam1);

                        if (matches.Where(i => (i.firstTeam == team && i.secondTeam == otherTeam) || (i.firstTeam == otherTeam && i.secondTeam == team)).Count() == 2) // maximum matches per team
                            continue;


                        matches.Add(new Match
                        {
                            firstTeam = team,
                            secondTeam = otherTeam,
                            date = newDate,
                            ground = ground,
                            day = newDate.DayOfWeek.ToString(),
                            time = time,
                            division = "elite"
                        });
                        count++;


                        break;

                    case 1: // first division


                        // check if all matches have happened 

                        if (matches.Where(i => i.division == "first").Count() == totalNumberOfMatchesInFirst)
                            continue;

                        ranTeam = rand.Next(0, 3);

                        team = first.ElementAt(ranTeam);

                        if (matches.Where(i => i.firstTeam == team || i.secondTeam == team).Count() >= maximumMatchesPerTeamFirst) // maximum matches per team
                            continue;

                        ranTeam1 = rand.Next(0, 3);

                        if (ranTeam == ranTeam1)
                        {
                            while (ranTeam == ranTeam1)
                            {
                                ranTeam1 = rand.Next(0, 3);
                            }
                        }

                        otherTeam = first.ElementAt(ranTeam1);

                        if (matches.Where(i => (i.firstTeam == team && i.secondTeam == otherTeam) || (i.firstTeam == otherTeam && i.secondTeam == team)).Count() == 2) // maximum matches per team
                            continue;


                        matches.Add(new Match
                        {
                            firstTeam = team,
                            secondTeam = otherTeam,
                            date = newDate,
                            ground = ground,
                            day = newDate.DayOfWeek.ToString(),
                            time = time,
                            division = "first"
                        });
                        count++;

                        break;

                    default:
                        break;

                }

            }


            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }


    }
}
