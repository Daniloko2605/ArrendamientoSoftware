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
            Usuarios? usuario = await _usuariosService.GetUsuariosAsync("daniel@gmail.com");

            if (usuario is null)
            {
                ArrendamientoSoftwareRole adminRole = _context.ArrendamientoSoftwareRoles.FirstOrDefault(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

                usuario = new Usuarios
                {
                    Email = "daniel@yopmail.com",
                    Nombre = "Daniel",
                    Apellido = "Ruiz",
                    PhoneNumber = "3105829748",
                    UserName = "manuel@yopmail.com",
                    Documento = "11111",
                    ArrendamientoSoftwareRole = adminRole
                };

                await _usuariosService.AddUsuariosAsync(usuario, "1234");

                string token = await _usuariosService.GenerateEmailConfirmationTokenAsync(usuario);
                await _usuariosService.ConfirmEmailAsync(usuario, token);
            }

            // Content Manager
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

                string token = await _usuariosService.GenerateEmailConfirmationTokenAsync(usuario);
                await _usuariosService.ConfirmEmailAsync(usuario, token);
            }
        }

        private async Task CheckRoles()
        {
            await AdminRoleAsync();
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
                await _context.SaveChangesAsync();
            }
        }

        private async Task ContentManagerAsync()
        {
            bool exists = await _context.ArrendamientoSoftwareRoles.AnyAsync(r => r.Name == "Gestor de contenido");

            if (!exists)
            {
                ArrendamientoSoftwareRole role = new ArrendamientoSoftwareRole { Name = "Gestor de contenido" };
                await _context.ArrendamientoSoftwareRoles.AddAsync(role);
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
