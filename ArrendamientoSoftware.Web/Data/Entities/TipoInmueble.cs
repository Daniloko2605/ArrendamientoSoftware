using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArrendamientoSoftware.Web.Data.Entities
{
    public class TipoInmueble
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Local")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Local { get; set; }

        [Display(Name = "Oficina")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]

        public string Oficina { get; set; }

        [Display(Name = "Bodega")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]

        public string Bodega { get; set; }

        [Display(Name = "Casa")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]

        public string Casa { get; set; }

        [Display(Name = "Apartamento")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]

        public string Apartamento { get; set; }

        [Display(Name = "Finca")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]

        public string Finca { get; set; }

        public bool IsPublished { get; set; } = false;

        public Propiedades Propiedad { get; set; }
        public int PropiedadId { get; set; }

    }
}
