using ArrendamientoSoftware.Web.Services;

namespace ArrendamientoSoftware.Web.Data.Seeders
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsuariosService _usuariosService;

        public SeedDb(DataContext context, IUsuariosService usuariosService)
        {
            _context = context;
            _usuariosService = usuariosService;
        }

        public async Task SeedAsync()
        {
            await new PropiedadesSeeder(_context).SeedAsync();
            await new PermissionsSeeder(_context).SeedAsync();
            await new UsuariosRolesSeeder(_context, _usuariosService).SeedAsync();
        }
    }
}
