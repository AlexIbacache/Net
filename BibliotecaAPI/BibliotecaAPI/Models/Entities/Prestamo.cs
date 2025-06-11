using System.ComponentModel.DataAnnotations;

// Clase que representa el préstamo de un libro a un usuario.
// Incluye claves foráneas hacia las entidades 'Libro' y 'Usuario'.
// Se valida que los campos esenciales estén presentes al registrar un nuevo préstamo.
// También contempla la posibilidad de registrar la fecha de devolución.

namespace BibliotecaAPI.Models.Entities
{
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo 'LibroId' es obligatorio.")]
        public int LibroId { get; set; }

        public Libro? Libro { get; set; }

        [Required(ErrorMessage = "El campo 'UsuarioId' es obligatorio.")]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "El campo 'FechaPrestamo' es obligatorio.")]
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;

        public DateTime? FechaDevolucion { get; set; }
    }
}
