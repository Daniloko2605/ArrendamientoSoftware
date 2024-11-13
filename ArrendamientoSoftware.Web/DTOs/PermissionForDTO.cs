using ArrendamientoSoftware.Web.Data.Entities;

namespace ArrendamientoSoftware.Web.DTOs
{
    public class PermissionForDTO : Permission
    {
        public bool Selected { get; set; } = false;
    }
}
