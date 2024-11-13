using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ArrendamientoSoftware.Web.DTOs
{
    public class UsuariosDTO
    {
        public Guid Id { get; set; }

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

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Telefono { get; set; } = null!;

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El campo {0} deve ser un Email válido")]
        public string Email { get; set; } = null!;

        [Display(Name = "Fecha de Creación")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Fecha de Actualización")]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public string NombreCompleto => $"{Nombre} {Apellido}";

        public int ArrendamientoSoftwareRoleId { get; set; }

        public IEnumerable<SelectListItem>? ArrendamientoSoftwareRoles { get; set; }
    }
}
