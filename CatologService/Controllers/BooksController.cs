using Microsoft.AspNetCore.Mvc;
using CatologService.Models;
using Microsoft.EntityFrameworkCore;
using CatologService.Data;


namespace CatologService.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BooksController : ControllerBase
    {
      private readonly CatalogDbContext _context;
        public BooksController(CatalogDbContext context)
        { 
        _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book==null) return NotFound();
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = book.Id}, book);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Book book)
        {
            if (id != book.Id) return BadRequest();
            var exists = await _context.Books.AnyAsync(x=>x.Id==id);
            if(!exists) return NotFound();
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("id:int")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book==null) return NotFound();
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
