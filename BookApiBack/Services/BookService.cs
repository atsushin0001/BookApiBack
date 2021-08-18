using System.Data;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using BookApiBack.Models;
using System.Threading.Tasks;

namespace BookApiBack.Services
{
    public class BookService : IBookService
    {

        private IDbConnection connection;

        public BookService(IDbConnection connection)
        {
            this.connection = connection;
        }

        public async Task<List<BookItem>> GetAllBooksAsync()

        {
            string query = "SELECT * FROM bookitems";
            List<BookItem> bookItems = (await connection.QueryAsync<BookItem>(sql: query)).ToList();

            return bookItems;
        }

        public async Task<List<BookItem>> GetBooksByTitleAsync(string title)

        {
            string query = "SELECT * FROM bookitems WHERE title LIKE '%" + @title + "%'";
            var param = new { title = title, };
            List<BookItem> bookItems = (await connection.QueryAsync<BookItem>(sql: query, param)).ToList();

            return bookItems;
        }
    }
}