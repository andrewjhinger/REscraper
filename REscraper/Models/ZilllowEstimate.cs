using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REscraper.Models
{
    public class ZillowEstimate
    {
        public int ReturnCode { get; set; }
        public string ReturnCodeMessage { get; set; }
        public int ZillowId { get; set; }
        public string LinktoMap { get; set; }
        public string LinktoHomeDetails { get; set; }
        public string LinktoGraphsAndData { get; set; }
        public string LinktoComparables { get; set; }
        public decimal Estimate { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal ValueChange { get; set; }
        public int ValueChangeDurationInDays { get; set; }
        public decimal ValueRangeLow { get; set; }
        public decimal ValueRangeHigh { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string chartUrl { get; set; }
    }
}