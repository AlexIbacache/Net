using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models.Entities
{
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LibroId { get; set; }
        public Libro? Libro { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [Required]
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;

        public DateTime? FechaDevolucion { get; set; }
    }
}
