using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.Services;

namespace ArrendamientoSoftware.Web.Data.Seeders
{
    public class UsuariosRolesSeeder
    {
        private readonly DataContext _context;
        private readonly IUsuariosService _usuariosService;

        public UsuariosRolesSeeder(DataContext context, IUsuariosService usuariosService)
        {
            _context = context;
            _usuariosService = usuariosService;
        }

        public async Task SeedAsync()
        {
            await CheckRoles();
            await CheckUsers();
        }

        private async Task CheckUsers()
        {
            // Admin
<<<<<<< HEAD
            Usuarios? usuario = await _usuariosService.GetUserAsync("daniel@yopmail.com");
=======
            Usuarios? usuario = await _usuariosService.GetUsuariosAsync("daniel@gmail.com");
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886

            if (usuario is null)
            {
                ArrendamientoSoftwareRole adminRole = _context.ArrendamientoSoftwareRoles.FirstOrDefault(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

                usuario = new Usuarios
                {
                    Email = "daniel@yopmail.com",
                    Nombre = "Daniel",
                    Apellido = "Ruiz",
<<<<<<< HEAD
                    PhoneNumber = "3135400865",
                    UserName = "daniel@yopmail.com",
                    Documento = "11117785",
                    ArrendamientoSoftwareRole = adminRole
                };

                await _usuariosService.AddUserAsync(usuario, "1234");
=======
                    PhoneNumber = "3105829748",
                    UserName = "manuel@yopmail.com",
                    Documento = "11111",
                    ArrendamientoSoftwareRole = adminRole
                };

                await _usuariosService.AddUsuariosAsync(usuario, "1234");
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886

                string token = await _usuariosService.GenerateEmailConfirmationTokenAsync(usuario);
                await _usuariosService.ConfirmEmailAsync(usuario, token);
            }

            // Content Manager
<<<<<<< HEAD
            usuario = await _usuariosService.GetUserAsync("danielc@yopmail.com");

            if (usuario is null)
            {
                ArrendamientoSoftwareRole agentVRole = _context.ArrendamientoSoftwareRoles.FirstOrDefault(r => r.Name == "Agente de ventas");

                usuario = new Usuarios
                {
                    Email = "danielc@yopmail.com",
                    Nombre = "Daniel",
                    Apellido = "Cardona",
                    PhoneNumber = "3148569748",
                    UserName = "danielc@yopmail.com",
                    Documento = "114747485",
                    ArrendamientoSoftwareRole = agentVRole
                };

                await _usuariosService.AddUserAsync(usuario, "1234");
=======
            usuario = await _usuariosService.GetUsuariosAsync("nalens@gmail.com");

            if (usuario is null)
            {
                ArrendamientoSoftwareRole contentManagerRole = _context.ArrendamientoSoftwareRoles.FirstOrDefault(r => r.Name == "Gestor de contenido");

                usuario = new Usuarios
                {
                    Email = "anad@yopmail.com",
                    Nombre = "Nairo",
                    Apellido = "Noel",
                    PhoneNumber = "3458595784",
                    UserName = "nalens@gmail.com",
                    Documento = "15455858",
                    ArrendamientoSoftwareRole = contentManagerRole
                };

                await _usuariosService.AddUsuariosAsync(usuario, "1234");
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886

                string token = await _usuariosService.GenerateEmailConfirmationTokenAsync(usuario);
                await _usuariosService.ConfirmEmailAsync(usuario, token);
            }
        }

        private async Task CheckRoles()
        {
            await AdminRoleAsync();
<<<<<<< HEAD
            await AgentVRoleAsync();
            await PropertiesManagerAsync();
        }

        private async Task PropertiesManagerAsync()
        {
            bool exists = await _context.ArrendamientoSoftwareRoles.AnyAsync(r => r.Name == "Gestor de propiedades");

            if (!exists)
            {
                ArrendamientoSoftwareRole role = new ArrendamientoSoftwareRole { Name = "Gestor de propiedades" };
                await _context.ArrendamientoSoftwareRoles.AddAsync(role);

                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Propiedades").ToListAsync();

                foreach (Permission permission in permissions)
                {
                    _context.RolePermissions.Add(new RolePermission { Role = role, Permission = permission });
                }

=======
            await ContentManagerAsync();
            await UsuariosManagerAsync();
        }

        private async Task UsuariosManagerAsync()
        {
            bool exists = await _context.ArrendamientoSoftwareRoles.AnyAsync(r => r.Name == "Gestor de usuarios");

            if (!exists)
            {
                ArrendamientoSoftwareRole role = new ArrendamientoSoftwareRole { Name = "Gestor de usuarios" };
                await _context.ArrendamientoSoftwareRoles.AddAsync(role);
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886
                await _context.SaveChangesAsync();
            }
        }

<<<<<<< HEAD
        private async Task AgentVRoleAsync()
        {
            bool exists = await _context.ArrendamientoSoftwareRoles.AnyAsync(r => r.Name == "Agente de ventas");

            if (!exists)
            {
                ArrendamientoSoftwareRole role = new ArrendamientoSoftwareRole { Name = "Agente de ventas" };
                await _context.ArrendamientoSoftwareRoles.AddAsync(role);

                _context.ArrendamientoSoftwareRoles.Add(role);

                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Propiedades").ToListAsync();

                foreach (Permission permission in permissions)
                {
                    _context.RolePermissions.Add(new RolePermission { Role = role, Permission = permission });
                }

=======
        private async Task ContentManagerAsync()
        {
            bool exists = await _context.ArrendamientoSoftwareRoles.AnyAsync(r => r.Name == "Gestor de contenido");

            if (!exists)
            {
                ArrendamientoSoftwareRole role = new ArrendamientoSoftwareRole { Name = "Gestor de contenido" };
                await _context.ArrendamientoSoftwareRoles.AddAsync(role);
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886
                await _context.SaveChangesAsync();
            }
        }

        private async Task AdminRoleAsync()
        {
            bool exists = await _context.ArrendamientoSoftwareRoles.AnyAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

            if (!exists) 
            {
                ArrendamientoSoftwareRole role = new ArrendamientoSoftwareRole { Name = Env.SUPER_ADMIN_ROLE_NAME };
                await _context.ArrendamientoSoftwareRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
