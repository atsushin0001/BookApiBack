using System.Collections.Generic;
using System.Threading.Tasks;
using BookApiBack.Models;

namespace BookApiBack.Services
{
    public interface IBookService
    {
        Task<List<BookItem>> GetAllBooksAsync();
        Task<List<BookItem>> GetBooksByTitleAsync(string title);
        Task<BookItem> GetBookByIdAsync(long id);
        Task<BookItem> PostBookAsync(BookItem bookItem);
        Task<BookItem> PutBookAsync(long id, BookItem bookItem);
        Task<BookItem> DelBookAsync(long id);

    }
}