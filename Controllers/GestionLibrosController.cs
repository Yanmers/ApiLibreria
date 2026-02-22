using ApiLibreria.Data;
using ApiLibreria.Data.Repository;
using ApiLibreria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;

namespace ApiLibreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionLibrosController : ControllerBase
    {
        private readonly ILibroRepository _libroRepository;

        public GestionLibrosController(LibroDBContext libroDBContext, ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        [HttpGet]
        [Route("GetAll", Name = "GetAllLibros")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<Libro> GetLibros()
        {
            var libros = _libroRepository.GetAllLibros();

            return libros;
        }

        [HttpGet]
        [Route("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Libro> GetLibroById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var libro = _libroRepository.GetLibroById(id);

            if (libro == null)
            {
                return NotFound($"El libro con el {id} not es valido");
            }

            return Ok(libro);
        }

        [HttpPost]
        [Route("CreateLibro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LibrosDTO> CreateLibro([FromBody] LibrosDTO librosDTO)
        {
            if (librosDTO == null)
            {
                return BadRequest();
            }

            //int newId = _libroDBContext.Libros.LastOrDefault()!.Id + 1;

            Libro libro = new Libro
            {
                //Id = newId,
                Titulo = librosDTO.Titulo,
                Autor = librosDTO.Autor,
                Anho = librosDTO.Anho
            };

            //_libroDBContext.Add(libro);
            //_libroDBContext.SaveChangesAsync();

            _libroRepository.CreateLibro(libro);

            libro.Id = librosDTO.Id;

            return Ok(libro);
        }

        [HttpPatch]
        [Route("Update", Name = "UpdateLibro")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LibrosDTO> UpdateLibro([FromBody] LibrosDTO model)
        {
            if (model == null || model.Id <= 0)
            {
                return BadRequest();
            }


            var libroUpdate = _libroRepository.GetLibroById(model.Id);

            //libroUpdate.Id = model.Id;
            libroUpdate.Titulo = model.Titulo;
            libroUpdate.Autor = model.Autor;
            libroUpdate.Anho = model.Anho;

            //_libroDBContext.Update(libroUpdate);
            //_libroDBContext.SaveChanges();
            if (libroUpdate == null)
            {
                return NotFound($"El {model.Id} que estas intentando actualizar es un not found");
            }

            _libroRepository.UpdateLibro(libroUpdate);

            return Ok(libroUpdate);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var deleteLibro = _libroRepository.GetLibroById(id);

            if (deleteLibro == null)
            {
                return NotFound($"El {id} que ingresaste es null ");
            }

            _libroRepository.Delete(deleteLibro);


            return Ok(true);
        }
    }
}
