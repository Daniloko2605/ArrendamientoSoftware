using System.ComponentModel.DataAnnotations;

namespace ArrendamientoSoftware.Web.Data.Entities
    {
        public class Propiedades
        {
            [Key]
            public int Id { get; set; }

            [Display(Name = "Dirección")]
            [StringLength(255, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
            [Required(ErrorMessage = "El campo '{0}' es requerido.")]

            public string Direccion { get; set; }

            [Display(Name = "Ciudad")]
            [StringLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]

            public string Ciudad { get; set; }

            [Display(Name = "Precio")]
            [Required(ErrorMessage = "El campo '{0}' es requerido.")]

            public decimal Precio { get; set; }

            public string Descripcion { get; set; }

            [Display(Name = "Descripción")]

            //revisar, recordar que esto es una relacion 
            public string Owner { get; set; }
            public int? IdOwner { get; set; }  // Puede ser nulo si no hay propietario asignado, como el profe nos lo explico

            // Fechas de seguimiento
            [Display(Name = "Fecha de Creación")]
            public DateTime CreatedDate { get; set; }

            [Display(Name = "Fecha de Actualización")]
            public DateTime UpdatedDate { get; set; }
        }
    }
