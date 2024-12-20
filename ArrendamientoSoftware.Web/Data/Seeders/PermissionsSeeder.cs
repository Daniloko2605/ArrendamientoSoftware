<<<<<<< HEAD
using ArrendamientoSoftware.Web.Data.Entities;
﻿using ArrendamientoSoftware.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
=======
﻿using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Data.Entities;
using static System.Collections.Specialized.BitVector32;
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886

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
<<<<<<< HEAD
            List<Permission> permissions = [.. TipoInmueble(), .. Propiedades(), .. Usuarios()];

            foreach (Permission permission in permissions)
            {
                bool exists = await _context.Permissions.AnyAsync(p => p.Name == permission.Name
=======
            List<Permission> permissions = [.. Blogs(), .. Sections()];

            foreach (Permission permission in permissions)
            {
                bool exists = await _context.Permissions.AnyAsync(p => p.Name == permission.Name 
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886
                                                                        && p.Module == permission.Module);

                if (!exists)
                {
                    await _context.Permissions.AddAsync(permission);
                }
            }

            await _context.SaveChangesAsync();
        }

<<<<<<< HEAD
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
=======
        private List<Permission> Blogs()
        {
            return new List<Permission>
            {
                new Permission { Name = "showBlogs", Description = "Ver Blogs", Module = "Blogs" },
                new Permission { Name = "createBlogs", Description = "Crear Blogs", Module = "Blogs" },
                new Permission { Name = "editBlogs", Description = "Editar Blogs", Module = "Blogs" },
                new Permission { Name = "deleteBlogs", Description = "Eliminar Blogs", Module = "Blogs" },
            };
        }

        private List<Permission> Sections()
        {
            return new List<Permission>
            {
                new Permission { Name = "showSections", Description = "Ver Sections", Module = "Sections" },
                new Permission { Name = "createSections", Description = "Crear Sections", Module = "Sections" },
                new Permission { Name = "editSections", Description = "Editar Sections", Module = "Sections" },
                new Permission { Name = "deleteSections", Description = "Eliminar Sections", Module = "Sections" },
            };
        }
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886
    }
}
