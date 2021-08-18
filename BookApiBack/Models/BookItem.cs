using System;

namespace BookApiBack.Models
{
    public class BookItem
    {
        public long id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string publisher { get; set; }
        public DateTime publisheddate { get; set; }
        public string image { get; set; }
    }   
}
