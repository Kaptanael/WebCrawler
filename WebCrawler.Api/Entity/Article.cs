using System;

namespace WebCrawler.Api.Entity
{
    public class Article
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool Archived { get; set; }
        public DateTime CrawledDate { get; set; }
    }
}
