using System.Collections.Generic;
using System.Threading.Tasks;
using BookApiBack.Models;

namespace BookApiBack.Services
{
    public interface IBookService
    {
        Task<List<BookItem>> GetAllBooksAsync();
        Task<List<BookItem>> GetBooksByTitleAsync(string title);
    }
}