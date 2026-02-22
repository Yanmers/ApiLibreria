
namespace ApiLibreria.Data.Repository
{
    public class LibroRepository : ILibroRepository
    {
        private readonly LibroDBContext _libroDBContext;
        public LibroRepository(LibroDBContext libroDBContext)
        {
            _libroDBContext = libroDBContext;
        }

        public int CreateLibro(Libro libro)
        {
            _libroDBContext.Libros.Add(libro);
            _libroDBContext.SaveChanges();
            return libro.Id;
        }

        public int UpdateLibro(Libro libro)
        {
            _libroDBContext.Update(libro);
            _libroDBContext.SaveChanges();
            return libro.Id;

        }

        public bool Delete(Libro libro)
        {
            _libroDBContext.Remove(libro);
            _libroDBContext.SaveChanges();
            return true;
        }

        public List<Libro> GetAllLibros()
        {
            return _libroDBContext.Libros.ToList();
        }

        public Libro GetLibroById(int id)
        {
            return _libroDBContext.Libros.Where(n => n.Id == id).FirstOrDefault();
        }
    }
}
