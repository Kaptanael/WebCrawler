using System;
using System.ComponentModel.DataAnnotations;

namespace WebCrawler.Api
{
    public class ArticleForCreateDto
    {   
        public DateTime Date { get; set; }

        [Required, StringLength(256)]
        public string Title { get; set; }

        [Required, StringLength(1024)]
        public string Url { get; set; }
        
        public bool Archived { get; set; }        
    }
}
