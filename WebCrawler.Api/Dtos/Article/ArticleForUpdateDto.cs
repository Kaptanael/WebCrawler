using System;
using System.ComponentModel.DataAnnotations;

namespace WebCrawler.Api
{
    public class ArticleForUpdateDto
    {
        [Required]
        public long Id { get; set; }

        public DateTime Date { get; set; }

        [Required, StringLength(256)]
        public string Title { get; set; }

        [Required, StringLength(1024)]
        public string Url { get; set; }

        public bool Archived { get; set; }
    }
}
