using APIBiblioteca.Context;
using APIBiblioteca.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBiblioteca.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public LibroController(ApplicationDBContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public ActionResult<List<Libro>> Get()
        {
            var libros = context.Libro.Include(x => x.Autor).ToList();
            return libros;
        }

        [HttpGet("{id}",Name = "ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = context.Libro.Include(x => x.Autor).FirstOrDefault(x => x.Id == id);

            if (libro == null)
            {
                return NotFound();
            }
            return libro;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            context.Libro.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.Id }, libro);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody]Libro libro, int id)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }
            context.Entry(libro).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = context.Libro.FirstOrDefault(x => x.Id == id);

            if (libro == null)
            {
                return NotFound();
            }
            context.Libro.Remove(libro);
            context.SaveChanges();
            return libro;
        }

    }
}
