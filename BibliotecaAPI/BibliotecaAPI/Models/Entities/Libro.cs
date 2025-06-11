using System.ComponentModel.DataAnnotations;

// Clase que representa un libro dentro del sistema de biblioteca.
// Contiene propiedades con validaciones que garantizan la integridad de los datos ingresados.
// Se relaciona con múltiples préstamos a través de la colección 'Prestamos'.

namespace BibliotecaAPI.Models.Entities
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo 'Título' es obligatorio.")]
        [StringLength(100, ErrorMessage = "El 'Título' no puede tener más de 100 caracteres.")]
        public string Titulo { get; set; }

        [StringLength(100, ErrorMessage = "El 'Autor' no puede tener más de 100 caracteres.")]
        public string Autor { get; set; }

        [StringLength(50, ErrorMessage = "El 'ISBN' no puede tener más de 50 caracteres.")]
        public string ISBN { get; set; }

        [Range(0, 2025, ErrorMessage = "El 'Año de publicación' debe ser un número positivo y valido (0 - año actual).")]
        public int AnioPublicacion { get; set; }

        public bool Disponible { get; set; } = true;

        [Required(ErrorMessage = "Debe existir una colección de préstamos, aunque esté vacía.")]
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}