using System;

namespace WebCrawler.Api
{
    public class ArticleForListDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool Archived { get; set; }
        public DateTime CrawledDate { get; set; }

    }
}
