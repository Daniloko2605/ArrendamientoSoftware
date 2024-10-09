using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Requests;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.Helpers;
using Microsoft.EntityFrameworkCore;


namespace ArrendamientoSoftware.Web.Services
{
    public interface IPropiedadesService
    {
        public Task<Response<Propiedades>> CreateAsync(Propiedades model);
        public Task<Response<Propiedades>> DeleteAsync(int id);
        public Task<Response<Propiedades>> EditAsync(Propiedades model);
        public Task<Response<List<Propiedades>>> GetListAsync();
        public Task<Response<Propiedades>> GetOneAsync(int id);
        public Task<Response<Propiedades>> TogglePropiedadesAsync(TogglePropiedadesRequest request);
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
                    Descripcion = model.Descripcion,
                    Direccion = model.Direccion,
                    Ciudad = model.Ciudad,
                    Precio = model.Precio,
                    Owner = model.Owner,
                    IdOwner = model.IdOwner,
                    CreatedDate = model.CreatedDate,
                    UpdatedDate = model.UpdatedDate,
                };

                await _context.Propiedades.AddAsync(propiedades);
                await _context.SaveChangesAsync();

                return ResponseHelper<Propiedades>.MakeResponseSuccess(propiedades, "Propiedad creada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Propiedades>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Propiedades>> DeleteAsync(int id)
        {
            try
            {
                Propiedades? propiedades = await _context.Propiedades.FirstOrDefaultAsync(s => s.Id == id);

                if (propiedades is null)
                {
                    return ResponseHelper<Propiedades>.MakeResponseFail($"La propiedad con el id '{id}' no existe.");
                }

                _context.Propiedades.Remove(propiedades);
                await _context.SaveChangesAsync();

                return ResponseHelper<Propiedades>.MakeResponseSuccess(null, "Propiedad eliminada con éxito");
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

                return ResponseHelper<Propiedades>.MakeResponseSuccess(model, "Propiedad actualizada con éxito");
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

                return ResponseHelper<List<Propiedades>>.MakeResponseSuccess(propiedades);
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
                    return ResponseHelper<Propiedades>.MakeResponseFail("No existe la propiedad con la descripción asignada");
                }

                return ResponseHelper<Propiedades>.MakeResponseSuccess(propiedades);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Propiedades>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Propiedades>> TogglePropiedadesAsync(TogglePropiedadesRequest request)
        {
            try
            {
                Propiedades? model = await _context.Propiedades.FindAsync(request.PropiedadDescripcion);

                if (model == null)
                {
                    return ResponseHelper<Propiedades>.MakeResponseFail($"No existe propiedad con la descripción '{request.PropiedadDescripcion}'");
                }

                _context.Propiedades.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Propiedades>.MakeResponseSuccess(model, "Propiedad Actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Propiedades>.MakeResponseFail(ex);
            }
        }
    }
}



