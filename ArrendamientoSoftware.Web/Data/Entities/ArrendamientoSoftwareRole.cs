using System.ComponentModel.DataAnnotations;

namespace ArrendamientoSoftware.Web.Data.Entities
{
    public class ArrendamientoSoftwareRole
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Rol")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        public ICollection<RolePermission> RolePermissions { get; set; }

        public IEnumerable<Usuarios> Usuarios { get; set; }
    }
}
