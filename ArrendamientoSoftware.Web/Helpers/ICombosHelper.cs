using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Data;

namespace ArrendamientoSoftware.Web.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboArrendamientoSoftwareRolesAsync();
        public Task<IEnumerable<SelectListItem>> GetComboPropiedades();
    }

    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboArrendamientoSoftwareRolesAsync()
        {
            List<SelectListItem> list = await _context.ArrendamientoSoftwareRoles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un rol...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboPropiedades()
        {
            List<SelectListItem> list = await _context.Propiedades.Select(s => new SelectListItem
            {
<<<<<<< HEAD
                Text = s.Descripcion,
=======
                Text = s.Tipo,
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886
                Value = s.Id.ToString()
            }).ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una propiedad]",
                Value = "0"
            });

            return list;
        }
    }
}
