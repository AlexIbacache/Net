using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaAPI.Data;
using BibliotecaAPI.Models.Entities;

// Controlador API para la gestión de préstamos de libros.
// Permite registrar nuevos préstamos y devolver libros (actualizando la fecha de devolución).
// Incluye validaciones para evitar duplicidad de devoluciones o referencias a libros/usuarios inexistentes.
// El método 'CreatedAtAction' depende de la existencia de un método GET que retorne un préstamo por ID.

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoesController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public PrestamoesController(BibliotecaContext context)
        {
            _context = context;
        }
        
        // GET: api/Prestamoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            return await _context.Prestamos.ToListAsync();
        }

        // GET: api/Prestamoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamo>> GetPrestamo(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);

            if (prestamo == null)
            {
                return NotFound();
            }

            return prestamo;
        }
        
        // PUT: api/Prestamoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestamo(int id, Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return BadRequest();
            }

            return NoContent();
        }
        
        // POST: api/Prestamoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prestamo>> PostPrestamo(Prestamo prestamo)
        {
            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrestamo", new { id = prestamo.Id }, prestamo);
        }
        //POST:/api/Devoluciones
        [HttpPost("devoluciones")]
        public async Task<IActionResult> PostDevolucion([FromBody] int prestamoId)
        {
            var prestamo = await _context.Prestamos.FindAsync(prestamoId);

            if (prestamo == null)
            {
                return NotFound($"No se encontró un préstamo con ID {prestamoId}.");
            }

            if (prestamo.FechaDevolucion != null)
            {
                return BadRequest("Este préstamo ya fue devuelto.");
            }

            prestamo.FechaDevolucion = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent(); // o return Ok(prestamo); si deseas retornar el objeto
        }
        
        // DELETE: api/Prestamoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamo(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }

            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
}
