using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawler.Api.Models
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
