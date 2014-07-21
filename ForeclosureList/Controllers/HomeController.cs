using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Postal;
using REscraper.Models;
using REscraper.Client;

namespace ForeclosureList.Controllers
{
    public class HomeController : Controller
    {

        private OhioReDbContext db = new OhioReDbContext();



        public void EmailUpdate()
        {
            var refresh = new RefreshDB();
            refresh.Scrape();

            var fProp = from s in db.REproperty
                        where s.AddDateAndTime == DateTime.Today
                        select s;


            dynamic email = new TypedEmail();
            email.To = "andrewjhinger@gmail.com";
            //email.Date = DateTime.UtcNow.ToString();
            email.Property = fProp.ToList();
            email.Send();
        }

    }


   
    public class TypedEmail : Email
    {
        public List<REproperty> Property { get; set; }
        
    }
}