using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

            var bingArticlesToCreate = await GetBingArticlesToCreateAsync(searchString);
            var bingResult = InsertArticle(bingArticlesToCreate);

            if (bingResult) 
            {
                Console.WriteLine("Bing articles has been inserted successfully.");
            }
            Console.ReadLine();
        }

        private static bool InsertArticle(List<ArticleForListDto> articles) 
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50000/api/articles/");                
                var postArticles = client.PostAsJsonAsync<List<ArticleForListDto>>("save-articles", articles);
                postArticles.Wait();

                var result = postArticles.Result;

                if (result.IsSuccessStatusCode) 
                {
                    return true;
                }

                return false;
            }
        }

        private static async Task<List<ArticleForListDto>> GetBingArticlesToCreateAsync(string searchString)
        {
            List<ArticleForListDto> articleForListDtos = null;

            var url = $"https://www.bing.com/search?q={searchString}&form=QBLH&sp=-1&pq=florida+man+august+23&sc=7-21&qs=n&sk=&cvid=B5B32D1BB6A240C9BAE1B87DB7C63235";
            
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            IEnumerable<HtmlNode> linkNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='b_title'] //h2 //a").ToList();

            if (linkNodes != null && linkNodes.Count() > 0) 
            {
                articleForListDtos = new List<ArticleForListDto>();

                foreach (HtmlNode link in linkNodes)
                {
                    ArticleForListDto articleForListDto = new ArticleForListDto();
                    articleForListDto.Date = DateTime.Now;
                    articleForListDto.Title = link.InnerText;
                    articleForListDto.Url = link.Attributes["href"].Value;
                    articleForListDto.Archived = false;
                    articleForListDtos.Add(articleForListDto);
                }
            }            

            return articleForListDtos;
        }
    }

    public class ArticleForListDto
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }        
        public string Url { get; set; }
        public bool Archived { get; set; }

    }
}
