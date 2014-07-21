using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REscraper;
using REscraper.Client;
using REscraper.Models;

namespace REscraper.Client
{
    public class RefreshDB
    {

        private IHTMLParser parser = new HTMLparser();

        private OhioReDbContext db = new OhioReDbContext();


        public void Scrape()
        {
            List<REproperty> properties = new List<REproperty>();
            var franklinZips = new List<string>(new string[]
             {
                 "43201"
                });

            var url = "";

            foreach (var zip in franklinZips)
            {
                url = String.Format("http://sheriff.franklincountyohio.gov/search/real-estate/printresults.aspx?q=searchType%3dZipCode%26searchString%3d{0}%26foreclosureType%3d%26sortType%3daddress%26saleDateFrom%3d{1}%2f{2}%2f{3}+12%3a00%3a00+AM%26saleDateTo%3d{4}%2f{5}%2f{6}+11%3a59%3a59+PM", zip, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Year, DateTime.Today.AddDays(180).Month, DateTime.Today.AddDays(180).Day, DateTime.Today.AddDays(180).Year);
                //Debug.WriteLine(url);
                var reproperty = parser.parseFranklinREpropertyHTML(url);
                properties.AddRange(reproperty);
            }

            properties.ForEach(s => db.REproperty.AddOrUpdate(p => p.PropertyAddress, s));
            db.SaveChanges();
        }

    }
}
