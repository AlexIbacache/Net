using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        // Relación con préstamos
        public ICollection<Prestamo> Prestamos { get; set; }
    }
}