using Microsoft.EntityFrameworkCore;

namespace BookApiBack.Models
{
    public class BookApiContext : DbContext
    {
        public BookApiContext(DbContextOptions<BookApiContext> options) : base(options)
        {
        }

        public DbSet<BookItem> bookitems { get; set; }
    }
}