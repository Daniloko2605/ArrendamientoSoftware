using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ArrendamientoSoftware.Web.Data.Entities
{
    public class Usuarios : IdentityUser 
    {
        [Display(Name = "Documento")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Documento { get; set; } = null!;

        [Display(Name = "Nombres")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Apellido { get; set; } = null!;

        public string NombreCompleto => $"{Nombre} {Apellido}";

        public int ArrendamientoSoftwareRoleId { get; set; }

        public ArrendamientoSoftwareRole ArrendamientoSoftwareRole { get; set; }
    }
}