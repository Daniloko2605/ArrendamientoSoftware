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
<<<<<<< HEAD
                new Propiedades { Descripcion = "Casa" },
                new Propiedades { Descripcion = "Local" },
                new Propiedades { Descripcion = "Apartamento" },
                new Propiedades { Descripcion = "Oficina" },
                new Propiedades { Descripcion = "Finca" },
            };

            foreach (Propiedades propiedad in propiedades)
            {
                bool exists = await _context.Propiedades.AnyAsync(s => s.Descripcion == propiedad.Descripcion);

                if (!exists)
                {
                    await _context.Propiedades.AddAsync(propiedad);
=======
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
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
