using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ArrendamientoSoftware.Web.Services
{
    public interface IPropertiesService
    {
        public Task<Response<Properties>> CreateAsync(Properties model);
        public Task<Response<Properties>> EditAsync(Properties model);
        public Task<Response<List<Properties>>> GetListAsync();
        public Task<Response<Properties>> GetOneAsync(int id);

    }

    public class PropertiesService : IPropertiesService
    {
        private readonly DataContext _context;

        public PropertiesService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Properties>> CreateAsync(Properties model)
        {
            try
            {
                Properties properties = new Properties
                {
                    Descripcion = model.Descripcion,
                };

                await _context.Properties.AddAsync(properties);
                await _context.SaveChangesAsync();

                return ResponseHelper<Properties>.MakeResponseSucess(properties, "Sección creada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Properties>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Properties>> EditAsync(Properties model)
        {
            try
            {

                _context.Properties.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Properties>.MakeResponseSucess(model, "Sección actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Properties>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<Properties>>> GetListAsync()
        {
            try
            {
                List<Properties> properties = await _context.Properties.ToListAsync();

                return ResponseHelper<List<Properties>>.MakeResponseSucess(properties);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<Properties>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Properties>> GetOneAsync(int id)
        {
            try
            {
                Properties? properties = await _context.Properties.FirstOrDefaultAsync(s => s.Id == id);

                if (properties is null)
                {
                    return ResponseHelper<Properties>.MakeResponseFail("La sección con el id indicado no existe");
                }

                return ResponseHelper<Properties>.MakeResponseSucess(properties);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Properties>.MakeResponseFail(ex);
            }
        }
    }
}



