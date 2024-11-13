using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Core.Pagination;
using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.Helpers;
using ArrendamientoSoftware.Web.Requests;
using Microsoft.EntityFrameworkCore;

namespace ArrendamientoSoftware.Web.Services
{
    public interface IPropiedadesService
    {
        public Task<Response<Propiedades>> CreateAsync(Propiedades model);

        public Task<Response<Propiedades>> DeleteAsync(int id);

        public Task<Response<Propiedades>> EditAsync(Propiedades model);

        public Task<Response<PaginationResponse<Propiedades>>> GetListAsync(PaginationRequest request);

        public Task<Response<Propiedades>> GetOneAsync(int id);

        public Task<Response<Propiedades>> ToggleAsync(TogglePropiedadesStatusRequest request);
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
                    Tipo = model.Tipo,
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
                Response<Propiedades> response = await GetOneAsync(id);

                if (!response.IsSuccess)
                {
                    return response;
                }

                _context.Propiedades.Remove(response.Result);
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

        public async Task<Response<PaginationResponse<Propiedades>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<Propiedades> query = _context.Propiedades.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.Tipo.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<Propiedades> list = await PagedList<Propiedades>.ToPagedListAsync(query, request);

                PaginationResponse<Propiedades> result = new PaginationResponse<Propiedades>
                {
                    List = list,
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter
                };

                return ResponseHelper<PaginationResponse<Propiedades>>.MakeResponseSuccess(result, "Secciones obtenidas con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<Propiedades>>.MakeResponseFail(ex);
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

                return ResponseHelper<Propiedades>.MakeResponseSuccess(propiedades);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Propiedades>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Propiedades>> ToggleAsync(TogglePropiedadesStatusRequest request)
        {
            try
            {
                Response<Propiedades> response = await GetOneAsync(request.PropiedadesId);

                if (!response.IsSuccess)
                {
                    return response;
                }

                Propiedades propiedades = response.Result;

                propiedades.IsHidden = request.Hide;
                _context.Propiedades.Update(propiedades);
                await _context.SaveChangesAsync();

                return ResponseHelper<Propiedades>.MakeResponseSuccess(null, "Propiedad actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Propiedades>.MakeResponseFail(ex);
            }
        }
    }
}



