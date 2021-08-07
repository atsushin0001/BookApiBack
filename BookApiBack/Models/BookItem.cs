using System;

namespace BookApiBack.Models
{
    public class BookItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Image { get; set; }
    }
}
