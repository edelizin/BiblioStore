using BiblioDomain.Interfaces;
using BiblioDomain.Services;
using BiblioInfrastructure.Context;
using BiblioInfrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BiblioAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<BiblioStoreDbContext>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBookService, BookService>();

            return services;
        }
    }
}
