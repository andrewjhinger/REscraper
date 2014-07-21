using System;
using System.Diagnostics;
using System.Net;
using System.Xml;
using REscraper.Models;

/// <summary>
/// ZillowAPI implementation for .Net
/// </summary>
public class ZillowAPI
{


    //public static ZillowEstimate GetZestimate(string zillowWebServiceId, string Address, string ZipCode)
    public static ZillowEstimate GetZestimate(string Address, string ZipCode)
    {
        //http://www.zillow.com/howto/api/GetSearchResults.htm

        var z = new ZillowEstimate();
        // Construct the url
        var zEstimate = new decimal();
        
        //var url = String.Format("http://www.zillow.com/webservice/GetSearchResults.htm?zws-id=X1-ZWz1dt06vqybkb_agge7&address={0}&citystatezip={1}", Address, ZipCode);
        var url = String.Format("http://www.zillow.com/webservice/GetSearchResults.htm?zws-id=X1-ZWz1b5ombnqvij_7aoby&address={0}&citystatezip={1}", Address, ZipCode);
        Debug.WriteLine(url);


        // Make the HTTP request / get the response
        var Request = (System.Net.HttpWebRequest)WebRequest.Create(url);
        var Response = (HttpWebResponse)Request.GetResponse();

        // Parse the HTTP response into an XML document
        XmlDocument xml = new XmlDocument();
        xml.Load(Response.GetResponseStream());
        XmlElement root = xml.DocumentElement;

        //Return Code
        z.ReturnCode = int.Parse(root.SelectSingleNode("//message/code").InnerText);
        z.ReturnCodeMessage = root.SelectSingleNode("//message/text").InnerText;


        if (z.ReturnCode == 0)
        {
            var selectSingleNode = root.SelectSingleNode("//response/results/result/zpid");
            if (selectSingleNode != null)
                z.ZillowId = int.Parse(selectSingleNode.InnerText);
            var singleNode = root.SelectSingleNode("//response/results/result/links/mapthishome");
            if (singleNode != null)
                z.LinktoMap = singleNode.InnerText;
            var xmlNode = root.SelectSingleNode("//response/results/result/links/homedetails");
            if (xmlNode != null)
                z.LinktoHomeDetails = xmlNode.InnerText;
            var node = root.SelectSingleNode("//response/results/result/links/graphsanddata");
            if (node != null)
                z.LinktoGraphsAndData = node.InnerText;
            var selectSingleNode1 = root.SelectSingleNode("//response/results/result/links/comparables");
            if (selectSingleNode1 != null)
                z.LinktoComparables = selectSingleNode1.InnerText;

            var node1 = root.SelectSingleNode("//response/results/result/zestimate/amount");
            decimal num;

            if (decimal.TryParse(node1.InnerText, out num))
            {
                z.Estimate = num;
            }


            var singleNode1 = root.SelectSingleNode("//response/results/result/zestimate/last-updated");
            if (singleNode1 != null)
                z.LastUpdated = DateTime.Parse(singleNode1.InnerText);


            //var xmlNode1 = root.SelectSingleNode("//response/results/result/zestimate/valueChange");
            //if (xmlNode1 != null)
            //    z.ValueChange = decimal.Parse(xmlNode1.InnerText);
            //var selectSingleNode2 = root.SelectSingleNode("//response/zestimate/valueChange");
            //if (selectSingleNode2 != null)
            //    z.ValueChangeDurationInDays = int.Parse(selectSingleNode2.Attributes["duration"].Value);
            var singleNode2 = root.SelectSingleNode("//response/results/result/zestimate/valuationRange/low");
            decimal num2;
            if (decimal.TryParse(singleNode2.InnerText, out num2))
            {
                z.ValueRangeLow = num2;
            }

            var selectSingleNode2 = root.SelectSingleNode("//response/results/result/zestimate/valuationRange/high");
            decimal num3;
            if (decimal.TryParse(selectSingleNode2.InnerText, out num3))
            {
                z.ValueRangeHigh = num3;
            }

            //z.Street = root.SelectSingleNode("//response/address/street").InnerText;
            //z.City = root.SelectSingleNode("//response/address/city").InnerText;
            //z.State = root.SelectSingleNode("//response/address/state").InnerText;
            //z.ZipCode = root.SelectSingleNode("//response/address/zipcode").InnerText;
            var xmlNode2 = root.SelectSingleNode("//response/results/result/address/latitude");
            
            decimal num4;
            if (decimal.TryParse(xmlNode2.InnerText, out num4))
            {
                z.Latitude = num4;
            }

            var xmlNode3 = root.SelectSingleNode("//response/results/result/address/longitude");

            decimal num5;
            if (decimal.TryParse(xmlNode3.InnerText, out num5))
            {
                z.Longitude = num5;
            }
        }
        Response.Close();

        return z;
    }

    public static ZillowEstimate GetChart(int zpid)
    {
        //http://www.zillow.com/howto/api/GetSearchResults.htm

        var z = new ZillowEstimate();
        // Construct the url
        //var zEstimate = new decimal();
        //var url = String.Format("http://www.zillow.com/webservice/GetChart.htm?zws-id=X1-ZWz1dt06vqybkb_agge7&unit-type=percent&zpid={}&width=300&height=150", zpid);
        var url = String.Format("http://www.zillow.com/webservice/GetChart.htm?zws-id=X1-ZWz1b5ombnqvij_7aoby&unit-type=percent&zpid={0}&width=300&height=150", zpid);
                                

        
        Debug.WriteLine(url);


        // Make the HTTP request / get the response
        var Request = (System.Net.HttpWebRequest)WebRequest.Create(url);
        var Response = (HttpWebResponse)Request.GetResponse();

        // Parse the HTTP response into an XML document
        XmlDocument xml = new XmlDocument();
        xml.Load(Response.GetResponseStream());
        XmlElement root = xml.DocumentElement;

        //Return Code
        z.ReturnCode = int.Parse(root.SelectSingleNode("//message/code").InnerText);
        z.ReturnCodeMessage = root.SelectSingleNode("//message/text").InnerText;


        if (z.ReturnCode == 0)
        {
            z.ZillowId = int.Parse(root.SelectSingleNode("//request/zpid").InnerText);
            z.chartUrl = root.SelectSingleNode("//response/url").InnerText;
        }
        Response.Close();

        return z;
    }
}