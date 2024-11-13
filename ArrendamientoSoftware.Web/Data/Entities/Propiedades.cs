using System.ComponentModel.DataAnnotations;

namespace ArrendamientoSoftware.Web.Data.Entities
{
        public class Propiedades
        {
            [Key]
            public int Id { get; set; }

            [Display(Name = "Propiedad")]
            [Required(ErrorMessage = "El campo '{0}' es requerido.")]
            public string Tipo { get; set; } = "DefaultTipo";

            [Display(Name = "Dirección")]
            [StringLength(255, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
            [Required(ErrorMessage = "El campo '{0}' es requerido.")]

            public string Direccion { get; set; } = "DefaultDireccion";

            [Display(Name = "Ciudad")]
            [StringLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]

            public string Ciudad { get; set; } = "DefaultCiudad";

            [Display(Name = "Precio")]
            [Required(ErrorMessage = "El campo '{0}' es requerido.")]

            public float Precio { get; set; }

            [Display(Name = "Descripción")]
            public string Descripcion { get; set; } = "DefaultDescripcion";

            [Display(Name = "Estado")]
           [StringLength(255, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
           [Required(ErrorMessage = "El campo '{0}' es requerido.")]
           public string Estado { get; set; } = "DefaultEstado";

        //revisar, recordar que esto es una relacion 
            public string Owner { get; set; } = "DefaultOwner";
            public int? IdOwner { get; set; }  // Puede ser nulo si no hay propietario asignado, como el profe nos lo explico

            // Fechas de seguimiento
            [Display(Name = "Fecha de Creación")]
            public DateTime CreatedDate { get; set; }

            [Display(Name = "Fecha de Actualización")]
            public DateTime UpdatedDate { get; set; }

            [Display(Name = "¿Está disponible?")]
            public bool IsHidden { get; set; }
<<<<<<< HEAD
        }
}
=======
    }
    }
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886
