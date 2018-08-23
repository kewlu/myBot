using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;

namespace MyBot.Models.Commands.AstrologyCommand
{
    public class Parser
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("p");
            foreach (var item in items)
            {
                list.Add(item.TextContent);

            }
            return list.ToArray();
        }
    }
}
