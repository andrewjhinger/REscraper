using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using System.Web.Script.Serialization;
//using System.Web.Script.Services;
//using System.Web.Services;

namespace REscraper.Models
{
    [Serializable]
    public class REproperty
    {
        public int ID { get; set; }

        public DateTime AddDateAndTime { get; set; }

        public string CaseNumber { get; set; }
        public string PropertyAddress { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public string Plaintiff { get; set; }
        public string Defendant { get; set; }
        public string Notes { get; set; }
        public string ForclosureType { get; set; }
        public string ParcelNumber { get; set; }
        public string PlantiffAtty { get; set; }
        public int AppraisedValue { get; set; }
        public int JudgmentValue { get; set; }
        public int OpeningBid { get; set; }
        public int Deposit { get; set; }
        public DateTime SaleDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public decimal ZestimateHigh { get; set; }
        public decimal ZestimateLow { get; set; }

        public int ZillowId { get; set; }
        public string LinktoMap { get; set; }
        public string LinktoHomeDetails { get; set; }
        public string LinktoGraphsAndData { get; set; }
        public string LinktoComparables { get; set; }
        public decimal Estimate { get; set; }
        public decimal ValueChange { get; set; }
        public int ValueChangeDurationInDays { get; set; }

        public string ChartUrl { get; set; }

       


    }
}