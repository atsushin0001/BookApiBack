using System.Data;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using BookApiBack.Models;
using System.Threading.Tasks;
using System;

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
            var param = new { title, };
            List<BookItem> bookItems = (await connection.QueryAsync<BookItem>(sql: query, param: param)).ToList();

            // LINQ確認用
            //Console.WriteLine("①title of first data: " + bookItems.First().title);
            //Console.WriteLine("①title of last data: " + bookItems.Last().title);
            //Console.WriteLine("②first title in ascending order of name: " + bookItems.OrderBy(b => b.title).First().title);
            //Console.WriteLine("②first title in descending order of name: " + bookItems.OrderByDescending(b => b.title).First().title);
            //Console.WriteLine("③max of title: " + bookItems.Max(b => b.title));
            //Console.WriteLine("③minimum of title: " + bookItems.Min(b => b.title));
            //Console.WriteLine("③max of id: " + bookItems.Max(b => b.id));
            //Console.WriteLine("③minimum of id: " + bookItems.Min(b => b.id));
            //Console.WriteLine("④average of id: " + bookItems.Average(b => b.id));
            //Console.WriteLine("④sum of id: " + bookItems.Sum(b => b.id));
            //Console.WriteLine("④count of id: " + bookItems.Count());
            //Console.WriteLine("⑤skip: " + bookItems.Skip(13).First().title + "：：" + bookItems.Skip(13).Last().title);
            //Console.WriteLine("⑤take: " + bookItems.Take(13).First().title + "：：" + bookItems.Take(13).Last().title);
            //Console.WriteLine("⑥ALL: " + bookItems.All(b => b.publisheddate >= DateTime.Parse("2022-1-1")));
            //Console.WriteLine("⑥ANY: " + bookItems.Any(b => b.publisheddate >= DateTime.Parse("2022-1-1")));
            //Console.WriteLine("⑥CONTAINS: " + bookItems.Contains(
            //    new BookItem() 
            //    { 
            //        id = 56, 
            //        title = "本１００１", 
            //        description = "アア", 
            //        publisher = "いい", 
            //        publisheddate = DateTime.Parse("2020-01-01"),
            //        image = "",
            //    }));
            //Console.WriteLine("⑦ANY: " + bookItems.Any(b => b.publisheddate >= DateTime.Parse("2022-1-1")));
            //Console.WriteLine("⑧Non reverse: ");
            //foreach (BookItem bi in bookItems) { Console.WriteLine(bi.title); };
            //Console.WriteLine("⑧Reverse: ");
            //bookItems.Reverse();
            //foreach (BookItem bi in bookItems) { Console.WriteLine(bi.title); };
            //bookItems = bookItems
            //    .Where(b => b.description.Contains("あいうえお"))
            //    .OrderBy(b => b.publisheddate)
            //    .Select(b => {
            //        BookItem newBookItem = b;
            //        if (b.publisher.Contains("山田"))
            //        {
            //            return b;
            //        }
            //        else 
            //        {
            //            b.description = b.description + "です。";
            //            return newBookItem;
            //        }
            //    })
            //    .ToList();
            //foreach (BookItem bi in bookItems) { Console.WriteLine(bi.title); };

            return bookItems;
        }

        public async Task<BookItem> GetBookByIdAsync(long id)
        {
            string query = "SELECT * FROM bookitems WHERE id = @id";
            var param = new { id, };
            BookItem bookItem = await connection.QueryFirstOrDefaultAsync<BookItem>(sql: query, param: param);

            return bookItem;
        }

        public async Task<BookItem> PostBookAsync(BookItem postBookItem)
        {
            string query = @"INSERT INTO bookitems (title,description,publisher,publisheddate,image) 
            VALUES (@title,@description, @publisher, @publisheddate,@image) RETURNING id;";
            BookItem param = new()
            {
                title = postBookItem.title,
                description = postBookItem.description,
                publisher = postBookItem.publisher,
                publisheddate = postBookItem.publisheddate,
                image = postBookItem.image,
            };
            long bookId = await connection.ExecuteScalarAsync<long>(sql: query, param: param);

            // insertの戻り値のIDで再検索し登録データを取得
            BookItem bookItem = await GetBookByIdAsync(bookId);

            return bookItem;
        }

        public async Task<BookItem> PutBookAsync(long putId, BookItem putBookItem)
        {
            string query = "UPDATE bookitems SET title = @title, description = @description, publisher = @publisher, publisheddate = @publisheddate, image = @image WHERE id = @id";
            BookItem param = new()
            {
                title = putBookItem.title,
                description = putBookItem.description,
                publisher = putBookItem.publisher,
                publisheddate = putBookItem.publisheddate,
                image = putBookItem.image,
                id = putId,
            };
            BookItem bookItem = await connection.ExecuteScalarAsync<BookItem>(sql: query, param: param);

            return bookItem;
        }

        public async Task<BookItem> DelBookAsync(long delId)
        {
            string query = "DELETE FROM bookitems WHERE id = @id";
            var param = new { id = delId };
            BookItem bookItem = await connection.ExecuteScalarAsync<BookItem>(sql: query, param: param);

            return bookItem;
        }
    }
}