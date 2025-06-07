using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models.Entities
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [StringLength(100)]
        public string Autor { get; set; }

        [StringLength(50)]
        public string ISBN { get; set; }

        public int AnioPublicacion { get; set; }
        public bool Disponible { get; set; } = true;

        // Relación con préstamos
        public ICollection<Prestamo> Prestamos { get; set; }
    }
}