using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using REscraper.Models;

namespace REscraper
{
    public interface IHTMLParser
    {
        List<REproperty> parseMadisionREpropertyHTML(string input);
        List<REproperty> parseLickingREpropertyHTML(string input);
        List<REproperty> parseMorrowREpropertyHTML(string input);
        List<REproperty> parseFranklinREpropertyHTML(string input);
    }
}