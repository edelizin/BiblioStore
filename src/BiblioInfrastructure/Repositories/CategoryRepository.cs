using BiblioDomain.Interfaces;
using BiblioDomain.Models;
using BiblioInfrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioInfrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BiblioStoreDbContext context) : base(context)
        {

        }
    }
}
