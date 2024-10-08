using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArrendamientoSoftware.Web.Data.Entities
{

        public class User
        {
            [Key]
            public int Id { get; set; }

            [Display(Name = "Nombre")]
            [StringLength(80, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
            [Required(ErrorMessage = "El campo '{0}' es requerido.")]
            public string Nombre { get; set; }

            [Display(Name = "Email")]
            [StringLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
            [Required(ErrorMessage = "El campo '{0}' es requerido.")]
            [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
            public string Email { get; set; }

            [Display(Name = "Teléfono")]
            [StringLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
            public string Telefono { get; set; }

            [Display(Name = "Rol")]
            [StringLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
            public string Rol { get; set; }

            // Tracking dates
            [Display(Name = "Fecha de Creación")]
            public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

            [Display(Name = "Fecha de Actualización")]
            public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        }
 
}