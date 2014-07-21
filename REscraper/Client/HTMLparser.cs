using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using REscraper.Models;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Xml;
using System.Text.RegularExpressions;
using Geocoding;
using Geocoding.Google;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace REscraper.Client
{
    public class HTMLparser : IHTMLParser
    {
       
        //IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyDdNoHnWidJkUXJPU40uqVs2Z9j36elL8E" };
        IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyB2gf_PO9fptoH09jome7wtwfF29HfD4H4" };


        public List<REproperty> parseMadisionREpropertyHTML(string urlToScrape)
        {
            List<REproperty> reproperty = new List<REproperty>();
            var url = urlToScrape;
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlWeb().Load(url);

            foreach (HtmlNode col in htmlDoc.DocumentNode.SelectNodes("//table"))
            {

                foreach (HtmlNode row in htmlDoc.DocumentNode.SelectNodes(".//tr[position()>2]"))
                {
                    HtmlAgilityPack.HtmlNode spanNode = row.SelectSingleNode(".//td//span");
                    HtmlAgilityPack.HtmlNode nbspNode = row.SelectSingleNode(".//*[text()='&nbsp;']");

                    if (spanNode != null)
                    {
                        if (nbspNode == null)
                        {


                            REproperty reprop = new REproperty();
                            string date = row.SelectSingleNode(".//td[@width='55']").InnerText;
                            DateTime d;

                            if (DateTime.TryParse(date, out d))
                            {
                                reprop.SaleDate = d;
                            }

                            string apprvalue = row.SelectSingleNode(".//td[not(@width)]").InnerText;
                            apprvalue = apprvalue.Replace("$", string.Empty);
                            apprvalue = apprvalue.Replace(",", string.Empty);
                            apprvalue = apprvalue.Replace(".", string.Empty);
                            apprvalue = apprvalue.Trim();
                            int num;

                            if (int.TryParse(apprvalue, out num))
                            {
                                reprop.AppraisedValue = num;
                            }


                            reprop.Defendant = row.SelectSingleNode(".//td[@width='107']").InnerText;

                            string judgvalue = row.SelectSingleNode(".//td[@width='115']").InnerText;
                            judgvalue = judgvalue.Replace("$", string.Empty);
                            judgvalue = judgvalue.Replace(",", string.Empty);
                            judgvalue = judgvalue.Replace(".", string.Empty);
                            judgvalue = judgvalue.Trim();
                            int num2;

                            if (int.TryParse(judgvalue, out num2))
                            {
                                reprop.JudgmentValue = num2;
                            }
                            reprop.CaseNumber = row.SelectSingleNode("td[@width='75']").InnerText;
                            reprop.PropertyAddress = row.SelectSingleNode(".//td[@width='96']").InnerText;
                            reprop.City = row.SelectSingleNode(".//td[@width='84']").InnerText;
                            reprop.Plaintiff = row.SelectSingleNode(".//td[@width='88']").InnerText;
                            reprop.County = "Madison";
                            //Debug.WriteLine(reprop.PropertyAddress);
                            GoogleAddress[] addresses = (GoogleAddress[])geocoder.Geocode(reprop.PropertyAddress);
                            reprop.Latitude = addresses[0].Coordinates.Latitude;
                            reprop.Longitude = addresses[0].Coordinates.Longitude;

                            var zip = addresses.Select(a => a[GoogleAddressType.PostalCode]).First();
                            reprop.Zip = zip.ShortName;
                            //Debug.WriteLine(reprop.Zip);
                            var z = ZillowAPI.GetZestimate(reprop.PropertyAddress, reprop.Zip);
                            reprop.ZestimateHigh = z.ValueRangeHigh;
                            reprop.ZestimateLow = z.ValueRangeLow;
                            reprop.LinktoMap = z.LinktoMap;
                            reprop.LinktoHomeDetails = z.LinktoHomeDetails;
                            reprop.LinktoGraphsAndData = z.LinktoGraphsAndData;
                            reprop.LinktoComparables = z.LinktoComparables;
                            //var z = ZillowAPI.GetZestimate("X1-ZWz1dt06vqybkb_agge7", reprop.PropertyAddress, addresses[0].Coordinates.);
                            
                            reproperty.Add(reprop);
                        }
                    }
                }


            }
            return reproperty.ToList();
        }

        public List<REproperty> parseLickingREpropertyHTML(string urlToScrape)
        {

            List<REproperty> reproperty = new List<REproperty>();
            var url = urlToScrape;
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlWeb().Load(url);

            foreach (HtmlNode col in htmlDoc.DocumentNode.SelectNodes("//table[@id='cntMain_gdvResults']"))
            {

                foreach (HtmlNode row in htmlDoc.DocumentNode.SelectNodes(".//tr[contains(@class,'AltGridItem')]"))
                {
                    HtmlAgilityPack.HtmlNode statusNode = row.SelectSingleNode(".//*[text()='Withdrawn']");

                    if (statusNode == null)
                    {
                        //Debug.WriteLine(row.InnerHtml);
                        //span[@id="cntMain_gdvResults_lblCaseNumber_3"]
                        REproperty reprop = new REproperty();


                        string date = row.SelectSingleNode(".//span[contains(@id,'cntMain_gdvResults_lblCaseNumber')]").InnerText;
                        DateTime d;

                        if (DateTime.TryParse(date, out d))
                        {
                            reprop.SaleDate = d;
                        }

                        reprop.CaseNumber = row.SelectSingleNode(".//span[contains(@id,'cntMain_gdvResults_Label1')]").InnerText;
                        reprop.PropertyAddress = row.SelectSingleNode(".//span[contains(@id,'cntMain_gdvResults_Label3')]").InnerText;
                        reprop.City = row.SelectSingleNode(".//span[contains(@id,'cntMain_gdvResults_Label4')]").InnerText;

                        string apprvalue = row.SelectSingleNode(".//span[contains(@id,'cntMain_gdvResults_Label2')]").InnerText;
                        apprvalue = apprvalue.Replace("$", string.Empty);
                        apprvalue = apprvalue.Replace(",", string.Empty);
                        apprvalue = apprvalue.Replace(".", string.Empty);
                        apprvalue = apprvalue.Trim();
                        int num;

                        if (int.TryParse(apprvalue, out num))
                        {
                            reprop.AppraisedValue = num;
                        }

                        reprop.Notes = row.SelectSingleNode(".//span[contains(@id,'cntMain_gdvResults_Label5')]").InnerText;


                        reproperty.Add(reprop);
                    }
                        
                    
                }


            }


            

            return reproperty.ToList();
        }

        public List<REproperty> parseMorrowREpropertyHTML(string urlToScrape)
        {

            List<REproperty> reproperty = new List<REproperty>();
            var url = urlToScrape;
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlWeb().Load(url);

            foreach (HtmlNode col in htmlDoc.DocumentNode.SelectNodes("//table[@bordercolordark='#CC9933']"))
            {
                foreach (HtmlNode comment in htmlDoc.DocumentNode.SelectNodes(".//comment()"))
                {
                    comment.ParentNode.RemoveChild(comment);
                }

                foreach (HtmlNode row in htmlDoc.DocumentNode.SelectNodes(".//tr[position()>1]"))
                {
                    REproperty reprop = new REproperty();
                    Debug.WriteLine(row.InnerText);
                    HtmlAgilityPack.HtmlNode dateNode = row.SelectSingleNode(".//td[@width='101%']");

                    if (dateNode == null)
                    {

                     


                    

                    //        Debug.WriteLine(row.SelectSingleNode(".//td//comment()//font").InnerText);
                    reprop.CaseNumber = row.SelectSingleNode(".//td[@width='11%']").InnerText;
                    reprop.Defendant = row.SelectSingleNode(".//td[@width='17%']").InnerText;

                    //string addresscity = row.SelectSingleNode(".//td[@width='27%']").InnerText;
                    //addresscity = addresscity.Replace("ST", "ST,");
                   
                    //char[] delimitercomma = new char[] { ',', '-' };
                    //string[] parts = addresscity.Split(delimitercomma, StringSplitOptions.RemoveEmptyEntries);
                    //parts = Regex.Split(addresscity, @"\R\w\s\d\d/g");


                    //Debug.WriteLine(values);

                    //Debug.WriteLine(parts[0]);
                    //if (parts[1] != null)
                    //{

                    //    Debug.WriteLine((parts[1]));
                    //}

                    reprop.PropertyAddress = row.SelectSingleNode(".//td[@width='27%']").InnerText;
                    //reprop.City = parts[1];
                    }
                    reproperty.Add(reprop);

                }
            }
            
            return reproperty.ToList();

            }

        public List<REproperty> parseFranklinREpropertyHTML(string urlToScrape)
        {



            List<REproperty> reproperty = new List<REproperty>();
            var url = urlToScrape;
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlWeb().Load(url);

            //Loop through the table and rows to pull out data

            var node = htmlDoc.DocumentNode.SelectSingleNode(".//*[text()='No records found.']");
            
            if (node == null)
            {
                //Debug.WriteLine("col start");
                //Debug.WriteLine(node.InnerText);
                //Debug.WriteLine(node.InnerHtml);
                //Debug.WriteLine("col end");

                foreach (HtmlNode col in htmlDoc.DocumentNode.SelectNodes("//table[@class='PrintDataGrid']"))
                {
                    if (col.InnerText != "No records found.")
                    {

                        foreach (
                            HtmlNode row in
                                htmlDoc.DocumentNode.SelectNodes(".//tr[contains(@class,'Printdatagridtext')]"))
                        {
                            REproperty reprop = new REproperty();
                            reprop.CaseNumber =
                                row.SelectSingleNode(
                                    ".//span[contains(@id,'c_printsearchresults_gvResults_lblCaseNum')]")
                                    .InnerText;
                            reprop.ForclosureType =
                                row.SelectSingleNode(
                                    ".//span[contains(@id,'c_printsearchresults_gvResults_lblForclosureType')]")
                                    .InnerText;


                            reprop.PropertyAddress =
                                row.SelectSingleNode(
                                    ".//span[contains(@id,'c_printsearchresults_gvResults_lblAddrNbr')]")
                                    .InnerText + " " +
                                row.SelectSingleNode(
                                    ".//span[contains(@id,'c_printsearchresults_gvResults_lblAddrStrName')]").InnerText +
                                ", " +
                                row.SelectSingleNode(
                                    ".//span[contains(@id,'c_printsearchresults_gvResults_lblAddrCity')]")
                                    .InnerText + " " +
                                row.SelectSingleNode(
                                    ".//span[contains(@id,'c_printsearchresults_gvResults_lblAddrState')]")
                                    .InnerText + " " +
                                row.SelectSingleNode(
                                    ".//span[contains(@id,'c_printsearchresults_gvResults_lblAddrZip')]")
                                    .InnerText;

                            var shortAddress = row.SelectSingleNode(
                                ".//span[contains(@id,'c_printsearchresults_gvResults_lblAddrNbr')]")
                                .InnerText + " " +
                                               row.SelectSingleNode(
                                                   ".//span[contains(@id,'c_printsearchresults_gvResults_lblAddrStrName')]")
                                                   .InnerText +
                                               ", " +
                                               row.SelectSingleNode(
                                                   ".//span[contains(@id,'c_printsearchresults_gvResults_lblAddrCity')]")
                                                   .InnerText;

                            reprop.Zip = row.SelectSingleNode(
                                    ".//span[contains(@id,'c_printsearchresults_gvResults_lblAddrZip')]")
                                    .InnerText;
                            reprop.ParcelNumber =
                                row.SelectSingleNode(".//span[contains(@id,'c_printsearchresults_gvResults_lblParcel')]")
                                    .InnerText;
                            reprop.Plaintiff =
                                row.SelectSingleNode(
                                    ".//span[contains(@id,'c_printsearchresults_gvResults_lblPlaintiffName')]")
                                    .InnerText;
                            reprop.PlantiffAtty =
                                row.SelectSingleNode(
                                    ".//span[contains(@id,'c_printsearchresults_gvResults_lblPlantiffAtty')]").InnerText;
                            reprop.County = "Franklin";

                            //Appraised, Opening, and Deposit

                            string apprvalue = row.SelectSingleNode(".//td[5]").InnerText;
                            apprvalue = apprvalue.Replace("$", string.Empty);
                            apprvalue = apprvalue.Replace(",", string.Empty);
                            apprvalue = apprvalue.Replace(".", string.Empty);
                            apprvalue = apprvalue.Trim();
                            int num;

                            if (int.TryParse(apprvalue, out num))
                            {
                                reprop.AppraisedValue = num;
                            }

                            string openingvalue = row.SelectSingleNode(".//td[6]").InnerText;
                            openingvalue = openingvalue.Replace("$", string.Empty);
                            openingvalue = openingvalue.Replace(",", string.Empty);
                            openingvalue = openingvalue.Replace(".", string.Empty);
                            openingvalue = openingvalue.Trim();
                            int numOpening;

                            if (int.TryParse(openingvalue, out numOpening))
                            {
                                reprop.OpeningBid = numOpening;
                            }

                            string depositvalue = row.SelectSingleNode(".//td[7]").InnerText;
                            depositvalue = depositvalue.Replace("$", string.Empty);
                            depositvalue = depositvalue.Replace(",", string.Empty);
                            depositvalue = depositvalue.Replace(".", string.Empty);
                            depositvalue = depositvalue.Trim();
                            int numdeposit;

                            if (int.TryParse(depositvalue, out numdeposit))
                            {
                                reprop.Deposit = numdeposit;
                            }

                            string date = row.SelectSingleNode(".//td[8]").InnerText;
                            DateTime d;

                            if (DateTime.TryParse(date, out d))
                            {
                                reprop.SaleDate = d;
                            }


                            GoogleAddress[] addresses = (GoogleAddress[])geocoder.Geocode(reprop.PropertyAddress);
                            //reprop.Latitude = addresses[0].Coordinates.Latitude;
                            //reprop.Longitude = addresses[0].Coordinates.Longitude;

                            //var zip = addresses.Select(a => a[GoogleAddressType.PostalCode]).First();
                            //reprop.Zip = zip.ShortName;

                            var streetAddress = addresses[0].FormattedAddress;
                            Debug.WriteLine(shortAddress);

                            var z = ZillowAPI.GetZestimate(shortAddress, reprop.Zip);
                            reprop.ZestimateHigh = z.ValueRangeHigh;
                            reprop.ZestimateLow = z.ValueRangeLow;
                            reprop.LinktoMap = z.LinktoMap;
                            reprop.LinktoHomeDetails = z.LinktoHomeDetails;
                            //reprop.LinktoGraphsAndData = z.LinktoGraphsAndData;
                            reprop.LinktoComparables = z.LinktoComparables;
                            reprop.ZillowId = z.ZillowId;

                            var c = ZillowAPI.GetChart(z.ZillowId);
                            reprop.ChartUrl = c.chartUrl;
                            reprop.AddDateAndTime = DateTime.Today;
                            //Debug.WriteLine(reprop.PropertyAddress);
                            //Debug.WriteLine(z.ValueRangeHigh);
                            //Debug.WriteLine(z.LinktoHomeDetails);

                            reproperty.Add(reprop);


                        }
                    }

                }
            }
            //Debug.WriteLine(responsePrint);
            return reproperty.ToList();
        }






        


            
        }
}




