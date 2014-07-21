using System;
using System.Linq;
using System.Web.Mvc;
using ForeclosureList.Controllers;
using Postal;
using REscraper.Models;

namespace REconsole
{
    class Program
    {
        
        static void Main(string[] args)
        {

            var foreclosureList = new HomeController();
            foreclosureList.EmailUpdate();
        }

        

    }
}
