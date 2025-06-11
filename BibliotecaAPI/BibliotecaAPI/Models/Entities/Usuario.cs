using System.ComponentModel.DataAnnotations;

// Clase que representa a un usuario del sistema de biblioteca.
// Contiene datos personales con validaciones, como nombre y correo electrónico.
// Se relaciona con múltiples préstamos a través de la colección 'Prestamos'.
// El correo electrónico debe tener un formato válido y no puede quedar vacío.

namespace BibliotecaAPI.Models.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo 'Nombre' es obligatorio.")]
        [StringLength(50, ErrorMessage = "El 'Nombre' no puede superar los 50 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El 'Apellido' no puede superar los 50 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo 'Email' es obligatorio.")]
        [StringLength(100, ErrorMessage = "El 'Email' no puede superar los 100 caracteres.")]
        [EmailAddress(ErrorMessage = "El formato del 'Email' no es válido.")]
        public string Email { get; set; }

        // Relación con préstamos
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
