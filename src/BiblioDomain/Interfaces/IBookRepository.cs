using BiblioDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiblioDomain.Interfaces
{
   public interface IBookRepository : IRepository<Book>
    {
        new Task<List<Book>> GetAll();
        new Task<Book> GetById(int id);
        Task<List<Book>> GetBooksByCategory(int categoryId);
        Task<List<Book>> SearchBookWithCategory(string value);


    }
   
}
