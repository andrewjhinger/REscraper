using System.Collections.Generic;
using System.Diagnostics;
using REscraper.Models;

namespace REscraper.Migrations
{
    using REscraper.Client;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<REscraper.Models.OhioReDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private IHTMLParser parser = new HTMLparser();
        protected override void Seed(REscraper.Models.OhioReDbContext context)
        {
            List<REproperty> properties = new List<REproperty>();
            var franklinZips = new List<string>(new string[]
             {
                 "43201"
                                //"43299","43291","43287","43279","43272","43271","43270","43268","43266","43265","43260","43251","43236","43235","43234","43232","43231","43230","43229","43228","43227","43226","43224","43223",                            "43222","43221","43220","43219","43218","43217","43216","43215","43214","43213","43212","43211","43210","43209","43207","43206","43205","43204","43203","43202","43201","43199","43198","43196    ","43195","43194","43137","43126","43125","43123","43119","43110","43109","43086","43085","43081","43069","43068","43054","43026","43017","43016","43004","43002"

                });

            var url = "";

            foreach (var zip in franklinZips)
            {
                url = String.Format("http://sheriff.franklincountyohio.gov/search/real-estate/printresults.aspx?q=searchType%3dZipCode%26searchString%3d{0}%26foreclosureType%3d%26sortType%3daddress%26saleDateFrom%3d{1}%2f{2}%2f{3}+12%3a00%3a00+AM%26saleDateTo%3d{4}%2f{5}%2f{6}+11%3a59%3a59+PM", zip, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Year, DateTime.Today.AddDays(180).Month, DateTime.Today.AddDays(180).Day, DateTime.Today.AddDays(180).Year);
                //Debug.WriteLine(url);
                var reproperty = parser.parseFranklinREpropertyHTML(url);
                properties.AddRange(reproperty);
            }

            //List<REproperty> properties = parser.parseMadisionREpropertyHTML("http://www.madisonsheriff.org/sheriffsales/sales.htm");
            properties.ForEach(s => context.REproperty.AddOrUpdate(p => p.CaseNumber, s));
            context.SaveChanges();
        }
    }
}
