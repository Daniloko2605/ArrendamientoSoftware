using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.Helpers;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;

namespace ArrendamientoSoftware.Web.Services
{
    public interface IPropiedadesService
    {
        public Task<Response<Propiedades>> CreateAsync(Propiedades model);
        public Task<Response<Propiedades>> EditAsync(Propiedades model);
        public Task<Response<List<Propiedades>>> GetListAsync();
        public Task<Response<Propiedades>> GetOneAsync(int id);

    }

    public class PropiedadesService : IPropiedadesService
    {
        private readonly DataContext _context;

        public PropiedadesService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Propiedades>> CreateAsync(Propiedades model)
        {
            try
            {
                Propiedades propiedades = new Propiedades
                {
                    Id = model.Id,
                };

                await _context.Propiedades.AddAsync(propiedades);
                await _context.SaveChangesAsync();

                return ResponseHelper<Propiedades>.MakeResponseSucess(propiedades, "Propiedad creada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Propiedades>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Propiedades>> EditAsync(Propiedades model)
        {
            try
            {

                _context.Propiedades.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Propiedades>.MakeResponseSucess(model, "Propiedad actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Propiedades>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<Propiedades>>> GetListAsync()
        {
            try
            {
                List<Propiedades> propiedades = await _context.Propiedades.ToListAsync();

                return ResponseHelper<List<Propiedades>>.MakeResponseSucess(propiedades);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<Propiedades>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Propiedades>> GetOneAsync(int id)
        {
            try
            {
                Propiedades? propiedades = await _context.Propiedades.FirstOrDefaultAsync(s => s.Id == id);

                if (propiedades is null)
                {
                    return ResponseHelper<Propiedades>.MakeResponseFail("La propiedad con el id indicado no existe");
                }

                return ResponseHelper<Propiedades>.MakeResponseSucess(propiedades);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Propiedades>.MakeResponseFail(ex);
            }
        }
    }
}
