namespace ApiLibreria.Data.Repository
{
    public interface ILibroRepository
    {
        List<Libro> GetAllLibros();

        Libro GetLibroById(int id);

        int CreateLibro(Libro libro);

        int UpdateLibro(Libro libro);

        bool Delete(Libro libro);
    }
}
