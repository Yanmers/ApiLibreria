using Microsoft.EntityFrameworkCore;

namespace ApiLibreria.Data
{
    public class LibroDBContext : DbContext
    {
        public LibroDBContext(DbContextOptions<LibroDBContext> options) : base(options)
        {

        }

        public DbSet<Libro> Libros { get; set; }
    }
}
