using AutoMapper;
using BiblioAPI.Dtos.Book;
using BiblioDomain.Interfaces;
using BiblioDomain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BiblioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IMapper mapper,
                                IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }
        // GET: api/<BooksController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResultDto>>> GetAll()
        {
            var books = await _bookService.GetAll();

            return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookResultDto>> Get(int id)
        {
            var book = await _bookService.GetById(id);

            if (book == null) return NotFound();

            return Ok(_mapper.Map<BookResultDto>(book));
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<ActionResult<BookResultDto>> Post([FromBody] BookResultDto bookResutlDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var book = _mapper.Map<Book>(bookResutlDto);
            var bookResult = await _bookService.Add(book);
           
            if (bookResult == null) return BadRequest();
            
            return Ok(book);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, BookEditDto bookDto)
        {
            if (id != bookDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            await _bookService.Update(_mapper.Map<Book>(bookDto));

            return Ok(bookDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var book = await _bookService.GetById(id);
            if (book == null) return NotFound();

            await _bookService.Remove(book);

            return Ok();
        }

        [Route("search/{bookName}")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> Search(string bookName)
        {
            var books = _mapper.Map<List<Book>>(await _bookService.Search(bookName));

            if (books == null || books.Count == 0) return NotFound("None book was founded");

            return Ok(books);
        }

        [Route("search-book-with-category/{searchedValue}")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> SearchBookWithCategory(string searchedValue)
        {
            var books = _mapper.Map<List<Book>>(await _bookService.SearchBookWithCategory(searchedValue));

            if (!books.Any()) return NotFound("None book was founded");

            return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));
        }
    }
}
