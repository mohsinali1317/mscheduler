using HtmlAgilityPack;
using MScheduler.Helpers;
using MScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MScheduler.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            List<string> elite = Constants.elite;
            List<string> first = Constants.first;

            List<Match> matches = new List<Match>();

            int totalNumberOfMatchesInElite = (elite.Count() * (elite.Count() - 1));
            int totalNumberOfMatchesInFirst = (first.Count() * (first.Count() - 1));

            int totalNumberOfMatches = totalNumberOfMatchesInElite + totalNumberOfMatchesInFirst;

            int maximumMatchesPerTeamElite = (elite.Count() - 1) * 2;
            int maximumMatchesPerTeamFirst = (first.Count() - 1) * 2;

            int count = 0;

            Random rand = new Random();
            Match m;

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

                        m = AddMatch("elite", matches, totalNumberOfMatchesInElite, maximumMatchesPerTeamElite, rand, elite, newDate, time, ground);

                        if (m == null)
                            continue;

                        matches.Add(m);
                        count++;

                        break;

                    case 1: // first division

                        m = AddMatch("first", matches, totalNumberOfMatchesInFirst, maximumMatchesPerTeamFirst, rand, first, newDate, time, ground);

                        if (m == null)
                            continue;

                        matches.Add(m);
                        count++;

                        break;

                    default:
                        break;

                }

            }

            ViewBag.matches = matches.OrderBy(i => i.division);

            /*   List<string> Eliteserie = new List<string>();
               List<string> FirstDivision = new List<string>();
               List<string> SecondDivision = new List<string>();
               List<string> ThirdDivision = new List<string>();
               List<string> FourthDivision = new List<string>();
               List<string> FifthDivision = new List<string>();
   */

            return View();

        }



        public Match AddMatch(string division, List<Match> matches, int totalNumberOfMatchesInDivision, int maximumMatchesPerTeamInDivision, Random rand,
            List<string> divisionList, DateTime matchDate, TimeSpan matchTime, string ground)
        {

            int ranTeam;

            string team;

            int ranTeam1;

            string otherTeam;

            // check if all matches have happened 

            int matchesInDivision = matches.Where(i => i.division.ToLower() == division.ToLower()).Count();

            if (matchesInDivision == totalNumberOfMatchesInDivision)
                return null;

            ranTeam = rand.Next(0, divisionList.Count);

            team = divisionList.ElementAt(ranTeam);

            if (matches.Where(i => i.firstTeam.ToLower() == team.ToLower() || i.secondTeam.ToLower() == team.ToLower()).Count() >= maximumMatchesPerTeamInDivision) // maximum matches per team
                return null;

            ranTeam1 = rand.Next(0, divisionList.Count);

            if (ranTeam == ranTeam1)
            {
                while (ranTeam == ranTeam1)
                {
                    ranTeam1 = rand.Next(0, divisionList.Count);
                }
            }

            otherTeam = divisionList.ElementAt(ranTeam1);

            if (matches.Where(i => (i.firstTeam.ToLower() == team.ToLower() && i.secondTeam.ToLower() == otherTeam.ToLower()) || (i.firstTeam.ToLower() == otherTeam.ToLower() && i.secondTeam.ToLower() == team.ToLower())).Count() == 2) // maximum matches per team
                return null;


            return new Match
            {
                firstTeam = Constants.ToTitleCase(team),
                secondTeam = Constants.ToTitleCase(otherTeam),
                date = matchDate,
                ground = Constants.ToTitleCase(ground),
                day = Constants.ToTitleCase(matchDate.DayOfWeek.ToString()),
                time = matchTime,
                division = Constants.ToTitleCase(division)
            };

        }

        public ActionResult Setup()
        {

            Constants.elite.Clear();
            Constants.first.Clear();

            WebClient webClient = new WebClient();
            HtmlDocument html = new HtmlDocument();
            html.Load(webClient.OpenRead("http://cricketforbundet.no/index.php/en/klubber"), Encoding.UTF8);
            var root = html.DocumentNode;


            var p = root.Descendants("table").FirstOrDefault().Descendants("tr").Skip(1);

            foreach (var item in p)
            {
                Constants.elite.Add(item.ChildNodes.Where(i => i.Name == "td").FirstOrDefault().InnerText);
                Constants.first.Add(item.ChildNodes.Where(i => i.Name == "td").ElementAt(1).InnerText);
                //SecondDivision.Add(item.ChildNodes.Where(i => i.Name == "td").ElementAt(2).InnerText);
                //ThirdDivision.Add(item.ChildNodes.Where(i => i.Name == "td").ElementAt(3).InnerText);
            }

            //var p1 = root.Descendants("table").ElementAt(1).Descendants("tr").Skip(2);

            //foreach (var item in p1)
            //{
            //    FourthDivision.Add(item.ChildNodes.Where(i => i.Name == "td").FirstOrDefault().InnerText);
            //    FifthDivision.Add(item.ChildNodes.Where(i => i.Name == "td").ElementAt(1).InnerText);
            //}

            //List<dynamic> teams = new List<dynamic>();
            //teams.Add(Eliteserie);
            //teams.Add(FirstDivision);
            //teams.Add(SecondDivision);
            //teams.Add(ThirdDivision);
            //teams.Add(FourthDivision);
            //teams.Add(FifthDivision);

            //ViewBag.teams = teams;




            return RedirectToAction("index");

        }


    }
}
