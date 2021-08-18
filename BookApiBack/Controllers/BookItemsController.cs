using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookApiBack.Models;
using Microsoft.Extensions.Logging;
using BookApiBack.Services;

namespace BookApiBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookItemsController : ControllerBase
    {
        private readonly BookApiContext _context;
        private readonly ILogger<BookItemsController> _logger;
        //private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IBookService _service;

        public BookItemsController(BookApiContext context, ILogger<BookItemsController> logger, IBookService service)
        {
            _context = context;
            _logger = logger;
            _service = service;
        }

        // GET: api/BookItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookItem>>> GetBookItemALL([FromQuery] string title)
        {
            _logger.LogInformation("Get using title!!!");

            if (title == null) 
            {
                var bookItems = await _service.GetAllBooksAsync();
                if (bookItems == null)
                {
                    return NotFound();
                }
                return Ok(bookItems);

                // if title is null, return all books.
                //return await _context.bookitems.ToListAsync();
            }
            else 
            {
                var bookItems = await _service.GetBooksByTitleAsync(title);
                if (bookItems == null)
                {
                    return NotFound();
                }
                return Ok(bookItems);

                // if title is not null, return books which contains that title name.
                //var bookItems = await _context.bookitems.Where(b => b.title.Contains(title)).ToListAsync();
                //if (bookItems == null)
                //{
                //    return NotFound();
                //}
                //return bookItems;

            }
        }

        // GET: api/BookItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookItem>> GetBookItem(long id)
        {
            _logger.LogInformation("Get using id!!!");

            var bookItem = await _context.bookitems.FindAsync(id);

            if (bookItem == null)
            {
                return NotFound();
            }

            return bookItem;
        }

        // GET: api/BookItems/publisheddate_start/publisheddate_end
        //[HttpGet("{publisheddate_start}/{publisheddate_end}")]
        //public async Task<ActionResult<IEnumerable<BookItem>>> GetBookItems(string publisheddate_start, string publisheddate_end)
        //{
        //    _logger.LogInformation("Get using date of span!!!");

        //    string sql = "SELECT * FROM bookitems";
        //    var people = connection.Query<Person>(sql)
        //                   .OrderBy(p => p.Age);

        //    var bookItems = await _context.bookitems.Where(b => b.title.Contains(keyword)).ToListAsync();

        //    if (bookItems == null)
        //    {
        //        return NotFound();
        //    }

        //    return bookItems;
        //}

        // PUT: api/BookItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookItem(long id, BookItem bookItem)
        {
            _logger.LogInformation("Put!!!");

            if (id != bookItem.id)
            {
                return BadRequest();
            }

            _context.Entry(bookItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BookItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookItem>> PostBookItem(BookItem bookItem)
        {
            _logger.LogInformation("Post!!!");


            _context.bookitems.Add(bookItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookItemALL), new { id = bookItem.id }, bookItem);
        }

        // DELETE: api/BookItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookItem(long id)
        {
            var bookItem = await _context.bookitems.FindAsync(id);
            if (bookItem == null)
            {
                return NotFound();
            }

            _context.bookitems.Remove(bookItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookItemExists(long id)
        {
            return _context.bookitems.Any(e => e.id == id);
        }
    }
}
