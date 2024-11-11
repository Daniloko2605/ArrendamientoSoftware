using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Data.Entities;

namespace ArrendamientoSoftware.Web.Data.Seeders
{
    public class PropiedadesSeeder
    {
        private readonly DataContext _context;

        public PropiedadesSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Propiedades> propiedades = new List<Propiedades>
            {
                new Propiedades { Tipo = "Bodega" },
                new Propiedades { Tipo = "Apartamento" },
                new Propiedades { Tipo = "Casa" },
                new Propiedades { Tipo = "Finca"},
            };

            foreach (Propiedades Propiedades in propiedades)
            {
                bool exists = await _context.Propiedades.AnyAsync(s => s.Tipo == Propiedades.Tipo);

                if (!exists) 
                {
                    await _context.Propiedades.AddAsync(Propiedades);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
