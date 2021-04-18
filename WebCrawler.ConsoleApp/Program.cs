using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawler.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter Search Words: ");
            string inputString = Console.ReadLine();            
            string searchString = inputString.Replace(" ", "+");
            await GetHtmlAsync(searchString);
            Console.ReadLine();
        }

        private static async Task<string> GetHtmlAsync(string searchString)
        {
            var url = $"https://www.bing.com/search?q={searchString}&form=QBLH&sp=-1&pq=florida+man+august+23&sc=7-21&qs=n&sk=&cvid=B5B32D1BB6A240C9BAE1B87DB7C63235";
            
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var li = htmlDocument.DocumentNode.Descendants("li")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("b_algo")).ToList();

            //var productTitle = htmlDocument.DocumentNode.Descendants("div")
            //     .Where(node => node.GetAttributeValue("class", "")
            //     .Equals("title")).ToList();

            //var productStatus = htmlDocument.DocumentNode.Descendants("p")
            //     .Where(node => node.GetAttributeValue("class", "")
            //     .Equals("productDetails-status")).ToList();

            //return productTitle[0].InnerHtml;

            return html;
        }
    }
}
