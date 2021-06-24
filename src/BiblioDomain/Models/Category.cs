using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioDomain.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }

        // EF relations
        public IEnumerable<Book> Books { get; set; }
    }
}
