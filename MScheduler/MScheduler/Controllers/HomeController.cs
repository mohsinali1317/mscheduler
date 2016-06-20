using HtmlAgilityPack;
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
            List<string> Eliteserie = new List<string>();
            List<string> FirstDivision = new List<string>();
            List<string> SecondDivision = new List<string>();
            List<string> ThirdDivision = new List<string>();
            List<string> FourthDivision = new List<string>();
            List<string> FifthDivision = new List<string>();


            // Creates an HtmlDocument object from an URL
            
            WebClient webClient = new WebClient();
            HtmlDocument html = new HtmlDocument();
            html.Load(webClient.OpenRead("http://cricketforbundet.no/index.php/en/klubber"), Encoding.UTF8);

            var root = html.DocumentNode;

            var p = root.Descendants("table").FirstOrDefault().Descendants("tr").Skip(1);

            foreach (var item in p)
            {
                Eliteserie.Add(item.ChildNodes.Where(i => i.Name == "td").FirstOrDefault().InnerText);
                FirstDivision.Add(item.ChildNodes.Where(i => i.Name == "td").ElementAt(1).InnerText);
                SecondDivision.Add(item.ChildNodes.Where(i => i.Name == "td").ElementAt(2).InnerText);
                ThirdDivision.Add(item.ChildNodes.Where(i => i.Name == "td").ElementAt(3).InnerText);
            }

            var p1 = root.Descendants("table").ElementAt(1).Descendants("tr").Skip(2);

            foreach (var item in p1)
            {
                FourthDivision.Add(item.ChildNodes.Where(i => i.Name == "td").FirstOrDefault().InnerText);
                FifthDivision.Add(item.ChildNodes.Where(i => i.Name == "td").ElementAt(1).InnerText);
            }

            List<dynamic> teams = new List<dynamic>();
            teams.Add(Eliteserie);
            teams.Add(FirstDivision);
            teams.Add(SecondDivision);
            teams.Add(ThirdDivision);
            teams.Add(FourthDivision);
            teams.Add(FifthDivision);

            ViewBag.teams = teams;

            return View();
        }

    }
}
