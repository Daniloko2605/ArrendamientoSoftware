using ArrendamientoSoftware.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArrendamientoSoftware.Web.Data.Seeders
{
    public class PermissionsSeeder
    {
        private readonly DataContext _context;

        public PermissionsSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Permission> permissions = [.. TipoInmueble(), .. Propiedades(), .. Usuarios()];

            foreach (Permission permission in permissions)
            {
                bool exists = await _context.Permissions.AnyAsync(p => p.Name == permission.Name
                                                                        && p.Module == permission.Module);

                if (!exists)
                {
                    await _context.Permissions.AddAsync(permission);
                }
            }

            await _context.SaveChangesAsync();
        }

        private List<Permission> TipoInmueble()
        {
            return new List<Permission>
            {
                new Permission { Name = "showInmuebles", Description = "Ver Inmuebles", Module = "Inmuebles" },
                new Permission { Name = "createInmuebles", Description = "Crear Inmuebles", Module = "Inmuebles" },
                new Permission { Name = "editInmuebles", Description = "Editar Inmuebles", Module = "Inmuebles" },
                new Permission { Name = "deleteInmuebles", Description = "Eliminar Inmuebles", Module = "Inmuebles" },
            };
        }

        private List<Permission> Propiedades()
        {
            return new List<Permission>
            {
                new Permission { Name = "showPropiedades", Description = "Ver Propiedades", Module = "Propiedades" },
                new Permission { Name = "createPropiedades", Description = "Crear Propiedades", Module = "Propiedades" },
                new Permission { Name = "editPropiedades", Description = "Editar Propiedades", Module = "Propiedades" },
                new Permission { Name = "deletePropiedades", Description = "Eliminar Propiedades", Module = "Propiedades" },
            };
        }

        private List<Permission> Usuarios()
        {
            List<Permission> list = new List<Permission>
            {
                new Permission { Name = "showUsuarios", Description = "Ver Usuarios", Module = "Usuarios" },
                new Permission { Name = "createUsuarios", Description = "Crear Usuarios", Module = "Usuarios" },
                new Permission { Name = "updateUsuarios", Description = "Editar Usuarios", Module = "Usuarios" },
                new Permission { Name = "deleteUsuarios", Description = "Eliminar Usuarios", Module = "Usuarios" },
            };

            return list;
        }
    }
}
